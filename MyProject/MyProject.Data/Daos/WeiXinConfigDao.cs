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
    public class WeiXinConfigDao : BaseDao<WeiXinConfig>
    {
        public PagedList<WeiXinConfig> GetPagedListConfig(string weixinId, int pageIndex, int pageSize)
        {
            var sql = Sql.Builder;
            if (!string.IsNullOrEmpty(weixinId))
            {
                sql.Where("weixinId=@0", weixinId);
            }
            sql.OrderBy("CreateTime desc");
            return PagedList<WeiXinConfig>(pageIndex, pageSize, sql);
        }

        public WeiXinConfig GetConfig(string weixinId)
        {
            var sql = Sql.Builder.Where("weixinId=@0", weixinId);
            return FirstOrDefault<WeiXinConfig>(sql);
        }

        public void DelConfig(string weixinId)
        {
            var sql = Sql.Builder.Append("delete WeiXinConfig where weixinId=@0", weixinId);
            Execute(sql);
        }

        public int AddConfig(WeiXinConfig model)
        {
            var sql = Sql.Builder.Append("insert into WeiXinConfig(Category,WeiXinId,AppId,Appsecret,ApiUrl,ApiToken,WeiXinName,MchId,PartnerKey,CertUrl,Remark,CreateTime,Creater) values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12)"
                , model.Category, model.WeiXinId, model.AppId, model.Appsecret, model.ApiUrl, model.ApiToken, model.WeiXinName,model.MchId,model.PartnerKey,model.CertUrl,model.Remark,model.CreateTime,model.Creater);
            return Execute(sql);
        }


        public int UpdateConfig(WeiXinConfig model)
        {
            var sql = Sql.Builder.Append("update WeiXinConfig set Category=@0,ApiUrl=@2,ApiToken=@3,WeiXinName=@4,MchId=@5,PartnerKey=@6,CertUrl=@7,Remark=@8,CreateTime=@9,Creater=@10 where WeiXinId=@1"
                , model.Category, model.WeiXinId, model.ApiUrl, model.ApiToken, model.WeiXinName, model.MchId, model.PartnerKey, model.CertUrl, model.Remark, model.CreateTime, model.Creater);
            return Execute(sql);
        }
    }
}
