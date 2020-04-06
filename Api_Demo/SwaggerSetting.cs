using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{
    public class SwaggerSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SwaggerGroupSetting> SwaggerGroups { get; set; }
        /// <summary>
        /// token名称
        /// </summary>
        public string TokenName { get; set; } = "Authorization";
        /// <summary>
        /// token描述
        /// </summary>
        public string TokenDescription { get; set; } = "Authorization format : Bearer {token}";

        /// <summary>
        /// api描述xml名
        /// </summary>
        public string ApiXmlName { get; set; }
        /// <summary>
        /// entity描述xml名
        /// </summary>
        public string EntityXmlName { get; set; }


    }
    /// <summary>
    /// 文档分组配置
    /// </summary>
    public class SwaggerGroupSetting
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 版本（分组名）
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 分组描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public string Name { get; set; }

    }
}
