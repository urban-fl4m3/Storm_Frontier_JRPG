using System;
using UnityEngine;

namespace SF.Common.Cinemachine
{
    public class CinemachineController : IDisposable
    {
        private readonly CinemachineView _view;
        private readonly CinemachineModel _model;

        public CinemachineController(CinemachineView view, CinemachineModel model)
        {
            _view = view;
            _model = model;
        }

        public void Init()
        {
            _model.Clear += OnClear;
            _model.TargetSet += OnTargetSet;
            _model.CameraPositionSet += OnCameraPositionSet;
        }

        public void Dispose()
        {
            _model.Clear -= OnClear;
            _model.TargetSet -= OnTargetSet;
            _model.CameraPositionSet -= OnCameraPositionSet;
        }

        private void OnCameraPositionSet(Transform target)
        {
            _view.MoveCamera(target);
        }

        private void OnTargetSet(Transform target, int index)
        {
            _view.SetTargetGroup(target, index);
        }

        private void OnClear()
        {
            _view.Clear();
        }
    }
}