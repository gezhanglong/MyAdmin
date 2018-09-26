using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// �洢ÿһ�������õ� Job ����ϸ��Ϣ 
    /// </summary>
    [TableName("QRTZ_JOB_DETAILS")]
    [PrimaryKey("SCHED_NAME,JOB_NAME,JOB_GROUP")]
    public class QRTZ_JOB_DETAILS
    {
         
        /// <summary>
        /// �����������
        /// </summary>
        public string SCHED_NAME { get; set; }
        
        /// <summary>
        /// job����
        /// </summary>
        public string JOB_NAME { get; set; }
        
        /// <summary>
        /// job������
        /// </summary>
        public string JOB_GROUP { get; set; }
        
        /// <summary>
        /// ����
        /// </summary>
        public string DESCRIPTION { get; set; }
        
        /// <summary>
        ///  jobʵ�������ȫ���� 
        /// </summary>
        public string JOB_CLASS_NAME { get; set; }
        
        /// <summary>
        /// �Ƿ�־û�,�Ѹ���������Ϊ1��quartz���job�־û������ݿ��� 
        /// </summary>
        public bool IS_DURABLE { get; set; }
        
        /// <summary>
        /// �Ƿ�Ⱥ
        /// </summary>
        public bool IS_NONCONCURRENT { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IS_UPDATE_DATA { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool REQUESTS_RECOVERY { get; set; }
        
        /// <summary>
        /// ��ų־û�job����  
        /// </summary>
        public byte[] JOB_DATA { get; set; }
        
    }
}