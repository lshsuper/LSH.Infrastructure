using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LSH.Infrastructure
{


    /*
     lt （less than）               小于
le （less than or equal to）   小于等于
eq （equal to）                等于
ne （not equal to）            不等于
ge （greater than or equal to）大于等于
gt （greater than）            大于
————————————————
版权声明：本文为CSDN博主「是逍遥呀呀呀呀」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/weixin_38092213/java/article/details/85315538
         
         */
    public class ConditionOption
    {

        public ConditionType ConditionType { get; set; }

        public string Left { get; set; }

        public   object Right { get; set; }

        public ConditionConnType ConnType { get; set; }
        
    }

    /// <summary>
    /// 条件类型
    /// </summary>
    public enum ConditionType
    {

        
        /// <summary>
        /// 等于
        /// </summary>
        Equal=0,
        /// <summary>
        /// 小于
        /// </summary>
        Less=1,
        /// <summary>
        /// 小于或等于
        /// </summary>
        LessOrEqual=2,
        /// <summary>
        /// 大于
        /// </summary>
        Greater = 3,
        /// <summary>
        /// 大于或等于
        /// </summary>
        GreaterOrEqual=4,
        /// <summary>
        /// 模糊匹配
        /// </summary>
        Like=5,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual=6


    }

    public enum ConditionConnType {
             
        And=0,
        Or=1
    
    }


}
