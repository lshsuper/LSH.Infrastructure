using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LSH.Infrastructure
{


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
