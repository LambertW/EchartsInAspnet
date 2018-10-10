using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchartsInAspnet.WindService.WindModels
{
    /// <summary>
    /// 交易状态
    /// </summary>
    public enum Trade_Status
    {
        连续停牌超过3天,
        停牌,
        暂停上市,
        正常交易
    }
}
