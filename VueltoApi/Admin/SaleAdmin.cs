using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueltoApi.Context;
using VueltoApi.Dto;
using VueltoApi.Models;

namespace VueltoApi.Admin
{
    public class SaleAdmin
    {
        private AppDbContext Context;
        public SaleAdmin(AppDbContext context)
        {
            this.Context = context;
        }

        public string SaveSale(decimal total, decimal PaidAmount)
        {
            try
            {
                if(total == 0)
                    return "El total no puede ser 0.";

                if (total > PaidAmount)
                    return "Total no puede ser mayor que monto pagado.";

                if (total < 0 || PaidAmount < 0)
                    return "No se aceptan valores negativos.";

                decimal balance = PaidAmount - total;
                var Banknotes = Context.Banknotes.Where(b => b.Value <= balance).OrderByDescending(x => x.Value).ToList();

                int i = 0;
                var result = new List<ChangeDetail>();

                while (balance > 0 && i < Banknotes.Count())
                {
                    if (balance >= Banknotes.ElementAt(i).Value)
                    {

                        var qty = (int)balance / (int)Banknotes.ElementAt(i).Value;
                        var newItem = new ChangeDetail();
                        newItem.Qty = qty;
                        newItem.Value = Banknotes.ElementAt(i).Value;
                        newItem.IsCoin = false;
                        newItem.DetailTotal = newItem.Qty * newItem.Value;
                        result.Add(newItem);

                        balance -= newItem.DetailTotal;
                    }
                    i++;
                }

                if (balance > 0)
                {
                    var monedas = Context.Coins.Where(c => c.Value <= balance).OrderByDescending(x => x.Value).ToList();
                    i = 0;

                    while (balance > 0)
                    {
                        if (balance >= monedas.ElementAt(i).Value)
                        {
                            var qty = balance / monedas.ElementAt(i).Value;
                            var newItem = new ChangeDetail();
                            newItem.Qty = (int)qty;
                            newItem.Value = monedas.ElementAt(i).Value;
                            newItem.IsCoin = true;
                            newItem.DetailTotal = newItem.Qty * newItem.Value;
                            result.Add(newItem);

                            balance -= newItem.DetailTotal;
                        }
                        i++;
                    }
                }

                var newSale = new Sale();
                newSale.Total = total;
                newSale.PaidAmount = PaidAmount;
                newSale.Change = PaidAmount - total;
                newSale.ChangeDetails = result;

                Context.Sale.Add(newSale);
                Context.SaveChanges();

                var msg = "Valor do Troco: R$ " + newSale.Change.ToString("0.00") + ".\nEntregar:\n";
                result.ForEach(i =>
                {
                    msg += i.Qty.ToString();
                    if (i.IsCoin)
                    {
                        if (i.Qty > 1)
                        {
                            msg += " moedas de R$" + i.Value.ToString("0.00") + ".\n";
                        }
                        else
                        {
                            msg += " moeda de R$" + i.Value.ToString("0.00") + ".\n";
                        }
                    }
                    else
                    {
                        if (i.Qty > 1)
                        {
                            msg += " notas de R$" + i.Value.ToString("0.00") + ".\n";
                        }
                        else
                        {
                            msg += " nota de R$" + i.Value.ToString("0.00") + ".\n";
                        }
                    }
                });

                return msg;
            }
            catch (Exception ex)
            {
                // SaveLog(ex.Message);
                return null;
            }
        }

        public SaleDto GetSaleById(int Id)
        {
            try
            {
                var sale = Context.Sale.Include(x => x.ChangeDetails).Where(s => s.Id == Id).FirstOrDefault();
                return ToDto(sale);
            }
            catch (Exception ex)
            {
                // SaveLog(ex.Message);
                return null;
            }
        }

        public List<SaleDto> GetAll()
        {
            try
            {
                var sales = Context.Sale.ToList();
                var result = new List<SaleDto>();
                for (int i = 0; i < sales.Count; i++)
                {
                    result.Add(ToDto(sales.ElementAt(i)));
                }
                return result;

            }
            catch (Exception ex)
            {
                // SaveLog(ex.Message);
                return null;
            }
        }

        public SaleDto ToDto(Sale sale)
        {
            var result = new SaleDto();
            result.Change = sale.Change;
            result.Id = sale.Id;
            result.PaidAmount = sale.PaidAmount;
            result.Total = sale.Total;

            if (sale.ChangeDetails != null)
            {
                result.ChangeDetails = new List<ChangeDetailDto>();

                for (int i = 0; i < sale.ChangeDetails.Count; i++)
                {
                    var detail = new ChangeDetailDto();
                    detail.DetailTotal = sale.ChangeDetails.ElementAt(i).DetailTotal;
                    detail.IsCoin = sale.ChangeDetails.ElementAt(i).IsCoin;
                    detail.Qty = sale.ChangeDetails.ElementAt(i).Qty;
                    detail.Value = sale.ChangeDetails.ElementAt(i).Value;

                    result.ChangeDetails.Add(detail);
                }


            }

            return result;
        }
    }
}
