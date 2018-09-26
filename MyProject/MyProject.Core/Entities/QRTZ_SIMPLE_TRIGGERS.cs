using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// �򵥵Ĵ�������ϸ��Ϣ
    /// </summary>
    [TableName("QRTZ_SIMPLE_TRIGGERS")]
    [PrimaryKey("SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP")]
    public class QRTZ_SIMPLE_TRIGGERS
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
        /// �ظ����� -1Ϊ����
        /// </summary>
        public int REPEAT_COUNT { get; set; }
        
        /// <summary>
        /// �ظ����
        /// </summary>
        public long REPEAT_INTERVAL { get; set; }
        
        /// <summary>
        /// �Ѵ�������
        /// </summary>
        public int TIMES_TRIGGERED { get; set; }
        
    }
}