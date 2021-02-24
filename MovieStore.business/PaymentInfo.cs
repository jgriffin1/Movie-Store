using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class PaymentInfo
  {
    public string RepId { get; set; }
    public string PaymentStatus { get; set; }
    public bool? IsVoid { get; set; }
    public int? CheckNumber { get; set; }
    public CreditCardType CreditCardTypeInformation { get; set; }

  }
}
