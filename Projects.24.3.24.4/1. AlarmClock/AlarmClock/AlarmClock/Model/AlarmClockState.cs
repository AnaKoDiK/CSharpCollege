using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Model
{
    internal class AlarmClockState
    {
        /// <summary>
        /// Время срабатывания
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// Сообщение при срабатывании
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Включен режим ожидания срабатывания
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Срабатывание сопровождается звуковым сигналом
        /// </summary>
        public bool IsSoundActive { get; set; }

        /// <summary>
        /// Срабатывание будильника зафиксировано
        /// </summary>
        public bool IsAwakeActivated { get; set; }

        /// <summary>
        /// Время повтора будильника
        /// </summary>
        public TimeSpan SnoozeInterval { get; set; } = TimeSpan.FromMinutes(10);

        /// <summary>
        /// Сколько был выполнен повтор будильника
        /// </summary>
        public int SnoozeCount { get; set; }

        /// <summary>
        /// Максимальное количество повторов будильника
        /// </summary>
        public int MaxSnoozeCount { get; set; } = 2;

        /// <summary>
        /// Проверка срабатывания будильника
        /// </summary>
        public void CheckAlarm()
        {
            if (!IsAlarmActive)
                return;

            DateTime now = DateTime.Now;

            if (!IsAwakeActivated && SnoozeCount == 0 && now >= AlarmTime)
            {
                IsAwakeActivated = true;
                return;
            }

            if (!IsAwakeActivated && SnoozeCount > 0 && SnoozeCount <= MaxSnoozeCount && now >= AlarmTime)
            {
                IsAwakeActivated = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void FinishRinging()
        {
            if (SnoozeCount < MaxSnoozeCount)
            {
                SnoozeCount++;
                AlarmTime = DateTime.Now + SnoozeInterval;
                IsAwakeActivated = false;
            }

            else
            {
                IsAlarmActive = false;
                IsAwakeActivated = false;
                SnoozeCount = 0;
            }
        }
    }
}
