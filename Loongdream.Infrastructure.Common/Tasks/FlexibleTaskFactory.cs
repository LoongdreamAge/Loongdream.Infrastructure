using System;
using System.Threading;
using System.Threading.Tasks;

namespace Loongdream.Infrastructure.Common.Tasks
{
    /// <summary>
    /// 灵活的任务工厂
    /// </summary>
    public class FlexibleTaskFactory
    {
        private readonly TaskFactory _factory;

        public FlexibleTaskFactory(int concurrencyLevel = 20)
        {
            _factory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(concurrencyLevel));
        }

        public Task AddTask(Action action)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return _factory.StartNew(action);
        }

        public Task AddTask(Action action, CancellationToken cancellToken)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return _factory.StartNew(action, cancellToken);
        }

        public Task AddTask(Action action, Action<Exception> failed)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return _factory.StartNew(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    failed(e);
                }

            });
        }

        public Task AddTask(Action action, Action<Exception> failed, CancellationToken cancellToken)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return _factory.StartNew(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    failed(e);
                }

            }, cancellToken);
        }

        public Task AddTask(Action action, Action success, Action<Exception> failed)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return _factory.StartNew(() =>
            {
                try
                {
                    action();
                    success();
                }
                catch (Exception e)
                {
                    failed(e);
                }

            });
        }

        public Task AddTask(Action action, Action success, Action<Exception> failed, CancellationToken cancellToken)
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            return _factory.StartNew(() =>
            {
                try
                {
                    action();
                    success();
                }
                catch (Exception e)
                {
                    failed(e);
                }

            }, cancellToken);
        }
    }
}