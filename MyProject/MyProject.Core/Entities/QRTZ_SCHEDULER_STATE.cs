using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// ������״̬��
    /// </summary>
    [TableName("QRTZ_SCHEDULER_STATE")]
    [PrimaryKey("SCHED_NAME,INSTANCE_NAME")]
    public class QRTZ_SCHEDULER_STATE
    {
        
        /// <summary>
        /// �����������
        /// </summary>
        public string SCHED_NAME { get; set; }
        
        /// <summary>
        /// �����ļ���org.quartz.scheduler.instanceId���õ����֣��������ΪAUTO,quartz�������������͵�ǰʱ�����һ�����֡�
        /// </summary>
        public string INSTANCE_NAME { get; set; }
        
        /// <summary>
        /// �ϴμ���ʱ��
        /// </summary>
        public long LAST_CHECKIN_TIME { get; set; }
        
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public long CHECKIN_INTERVAL { get; set; }
        
    }
}