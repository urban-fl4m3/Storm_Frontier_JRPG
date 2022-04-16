using System;
using System.Collections.Generic;
using System.Linq;
using SF.Common.Logger;

namespace SF.Common.Commands
{
    public class CommandQueue : ICommand
    {
        public event EventHandler OnComplete;
        public event EventHandler OnFail;
        
        private readonly Queue<ICommand> _queue = new Queue<ICommand>();
        private readonly IDebugLogger _logger;
        
        private int _completedCount;
        private int _totalCount;
        private bool _executing;

        public CommandQueue(IDebugLogger logger)
        {
            _logger = logger;
        }
        
        public void Execute()
        {
            if (_executing)
            {
                return;
            }
            
            _executing = true;
            _totalCount = _queue.Count;
            
            while (_queue.Any())
            {
                var command = _queue.Dequeue();
                command.OnComplete += OnCommandComplete;
                command.OnFail += OnCommandFailed;
                command.Execute();
            }
        }

        private void OnCommandFailed(object sender, EventArgs e)
        {
            ClearCommandSubscription((ICommand) sender);
            Clear();
            
            _logger.LogError($"[CommandQueue] Failed after {_completedCount} completed tasks!");
            OnFail?.Invoke(this, EventArgs.Empty);
        }

        private void OnCommandComplete(object sender, EventArgs e)
        {
            ClearCommandSubscription((ICommand) sender);
            
            if (++_completedCount >= _totalCount)
            {
                RaiseCompletion();
            }
        }

        private void RaiseCompletion()
        {
            Clear();
            OnComplete?.Invoke(this, EventArgs.Empty);
        }

        private void Clear()
        {
            _queue.Clear();
            _completedCount = 0;
            _totalCount = 0;
            _executing = false;
        }

        private void ClearCommandSubscription(ICommand command)
        {
            command.OnComplete -= OnCommandComplete;
            command.OnFail -= OnCommandFailed;
        }
    }
}