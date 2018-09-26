using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// ��������Ϣ��
    /// </summary>
    [TableName("QRTZ_TRIGGERS")]
    [PrimaryKey("SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP,JOB_NAME,JOB_GROUP")]
    public class QRTZ_TRIGGERS
    {
        
        /// <summary>
        /// �����������
        /// </summary>
        public string SCHED_NAME { get; set; }
        
        /// <summary>
        /// ����������
        /// </summary>
        public string TRIGGER_NAME { get; set; }
        
        /// <summary>
        /// ������������
        /// </summary>
        public string TRIGGER_GROUP { get; set; }
        
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
        /// �´�ִ��ʱ��
        /// </summary>
        public long? NEXT_FIRE_TIME { get; set; }
        
        /// <summary>
        /// �ϴ�ִ��ʱ��
        /// </summary>
        public long? PREV_FIRE_TIME { get; set; }
        
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int? PRIORITY { get; set; }
        
        /// <summary>
        /// ִ��״̬��WAITING��PAUSED��ACQUIRED�ֱ�Ϊ���ȴ�����ͣ��������
        /// </summary>
        public string TRIGGER_STATE { get; set; }
        
        /// <summary>
        /// ���������ͣ�simple��cron
        /// </summary>
        public string TRIGGER_TYPE { get; set; }
        
        /// <summary>
        /// ��ʼִ��ʱ��
        /// </summary>
        public long START_TIME { get; set; }
        
        /// <summary>
        /// ����ִ��ʱ��
        /// </summary>
        public long? END_TIME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CALENDAR_NAME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? MISFIRE_INSTR { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] JOB_DATA { get; set; }
        
    }
}