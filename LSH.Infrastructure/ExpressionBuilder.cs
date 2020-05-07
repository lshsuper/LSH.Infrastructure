using Google.Protobuf.WellKnownTypes;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LSH.Infrastructure
{
    public class ExpressionBuilder
    {

        /// <summary>
        /// 构建表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static Func<T,bool> Build<T>(IEnumerable<ConditionOption> opts)where T:class,new()
        {
            Expression body = null; ParameterExpression parameter = Expression.Parameter(typeof(T), "w");
            ConditionOption prevOpt=null;
            foreach (var opt in opts)
            {

                Expression left = null, right = null,express =null ;
                left = Expression.PropertyOrField(parameter,opt.Left);
                right = Expression.Constant(opt.Right);
                switch (opt.ConditionType)
                {
                    case ConditionType.Equal:
                        express = Expression.Equal(left, right);
                        break;
                    case ConditionType.Less:
                        express = Expression.LessThan(left, right);
                        break;
                    case ConditionType.LessOrEqual:
                        express = Expression.LessThanOrEqual(left, right);
                        break;
                    case ConditionType.Greater:
                        express = Expression.GreaterThan(left, right);
                        break;
                    case ConditionType.GreaterOrEqual:
                        express = Expression.GreaterThanOrEqual(left, right);
                        break;
                    case ConditionType.Like:
                        express = Expression.Call(left, typeof(String).GetMethod("Contains", new System.Type[] { typeof(string) }), right);
                        break;
                    case ConditionType.NotEqual:
                        express = Expression.NotEqual(left, right);
                        break;
                    default:
                        throw new NotSupportedException("不支持的条件类型");
                }

                if (body == null)
                {
                    body = express;
                }
                else
                {
                    switch (prevOpt.ConnType)
                    {
                        case ConditionConnType.And:
                            body = Expression.AndAlso(body, express);
                            break;
                        case ConditionConnType.Or:
                            body = Expression.OrElse(body, express);
                            break;
                        default:
                            throw new NotSupportedException("不支持的条件类型");
                    }

                }
                //前一个元素
                prevOpt = opt;
            }
            return Expression.Lambda<Func<T, bool>>(body, parameter).Compile();
        }

    }
}
