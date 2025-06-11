using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.Model
{
    public record ApiErrorResponse(List<ApiError> Errors);

    public record ApiError(string Code, string Description);
}
