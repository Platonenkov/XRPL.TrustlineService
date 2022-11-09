using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRPL.TrustlineService.Domain.Xls20
{
    public class Xls20NftResponse : BaseServerResponse
    {
        public Xls20Nft nft { get; set; }
    }
}
