using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class AllServerGroupDateResponse
{
    public string Value { get; set; }
    public string Text { get; set; }
    public object ImageFileId { get; set; }
    public object[] DataAttributes { get; set; }
}
