﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRPL.TrustlineService.Domain.Xls20
{
    public class Xls20NftResponse
    {
        public string nftokenid { get; set; }
        public Xls20Nft nft { get; set; }
    }
}
