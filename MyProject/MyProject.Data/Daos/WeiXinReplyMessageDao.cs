using MyProject.Core.Entities;
using MyProject.Services.MvcPager;
using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Data.Daos
{
    [DbFactory("MyP")]
    public class WeiXinReplyMessageDao : BaseDao<WeiXinReplyMessage>
    { 
        public PagedList<WeiXinReplyMessage> GetPagedList(int pageIndex, int pageSize)
        {
            var sql = Sql.Builder.OrderBy("CreateTime desc");
            return PagedList<WeiXinReplyMessage>(pageIndex, pageSize, sql);
        }

        public WeiXinReplyMessage GetById(int id)
        {
            var sql = Sql.Builder.Where("Id=@0",id);
            return FirstOrDefault<WeiXinReplyMessage>(sql);
        }

        public List<WeiXinReplyMessage> GetByReplayType()
        {
            var sql = Sql.Builder.Where("ReplayType is not null and replayType !='#'");
            return Query<WeiXinReplyMessage>(sql).ToList();
        }

        public void DeleteById(int id)
        {
            var sql = Sql.Builder.Where("Id = @0", id);
            Delete(sql);
        }

        /// <summary>
        /// 按关键字筛选
        /// </summary>
        /// <param name="matchKey"></param>
        /// <returns></returns>
        public WeiXinReplyMessage GetMessage(string matchKey)
        {
            var sql = Sql.Builder.Select("*").From("WeiXinReplyMessage").Where(string.Format("MatchKey like'%{0}%'", matchKey)); 
            return FirstOrDefault<WeiXinReplyMessage>(sql);
        }

        /// <summary>
        /// 按触发类型和关键字筛选
        /// </summary>
        /// <param name="replayType"></param>
        /// <returns></returns>
        public WeiXinReplyMessage GetMessageByReplayType(string replayType, string matchKey)
        {
            if (!string.IsNullOrEmpty(matchKey))
            {
                var sql = Sql.Builder.Select("*").From("WeiXinReplyMessage").Where("ReplayType=@0 and MatchKey=@1", replayType, matchKey);
                return FirstOrDefault<WeiXinReplyMessage>(sql);
            }
            else
            {
                var sql = Sql.Builder.Select("*").From("WeiXinReplyMessage").Where("ReplayType=@0", replayType);
                return FirstOrDefault<WeiXinReplyMessage>(sql);
            }
           
        }
    }
}
