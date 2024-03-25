using System;
using System.Threading.Tasks;

namespace Loongdream.Infrastructure.Common.Tasks
{
    /// <summary>
    /// 简易示例任务用法
    /// </summary>
    /// <remarks>线程上限固定为200，只是简易案例</remarks>
    public class SimpleTask
    {
        private const int ConcurrencyLevel = 200;
        private static readonly FlexibleTaskFactory Factory;

        static SimpleTask()
        {
            Factory = new FlexibleTaskFactory(ConcurrencyLevel);
        }

        /// <summary>
        /// 添加一个普通任务
        /// </summary>
        /// <param name="action">常规方法</param>
        /// <returns></returns>
        public static Task AddTask(Action action)
        {
            return Factory.AddTask(action);
        }

        /// <summary>
        /// 添加一个异常处理的任务
        /// </summary>
        /// <param name="action">常规方法</param>
        /// <param name="failed">异常处理</param>
        /// <returns></returns>
        public static Task AddTask(Action action, Action<Exception> failed)
        {
            return Factory.AddTask(action, failed);
        }

        /// <summary>
        /// 添加一个有成功回调和异常处理的任务
        /// </summary>
        /// <param name="action">常规方法</param>
        /// <param name="success">成功回调</param>
        /// <param name="failed">异常处理</param>
        /// <returns></returns>
        public static Task AddTask(Action action, Action success, Action<Exception> failed)
        {
            return Factory.AddTask(action, success, failed);
        }
    }
}