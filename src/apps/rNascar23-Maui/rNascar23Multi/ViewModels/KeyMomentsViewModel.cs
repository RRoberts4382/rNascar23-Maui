﻿using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace rNascar23Multi.ViewModels
{
    public class KeyMomentsViewModel : ObservableObject, INotifyUpdateTarget, IDisposable
    {
        ILogger<KeyMomentsViewModel> _logger;
        private IKeyMomentsRepository _keyMomentsRepository;

        private ObservableCollection<KeyMomentsModel> _models = new ObservableCollection<KeyMomentsModel>();

        public ObservableCollection<KeyMomentsModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Key Moments";
        public string ListHeader
        {
            get => _listHeader;
            set => SetProperty(ref _listHeader, value);
        }

        public KeyMomentsViewModel(
            ILogger<KeyMomentsViewModel> logger, 
            IKeyMomentsRepository keyMomentsRepository)
        {
            _keyMomentsRepository = keyMomentsRepository ?? throw new ArgumentNullException(nameof(keyMomentsRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region public

        public async Task UserSettingsUpdatedAsync()
        {

        }

        public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
        {
            try
            {
                _logger.LogInformation($"KeyMomentsViewModel - UpdateTimer_UpdateTimerElapsed");

                if (e.SessionDetails != null)
                {
                    await LoadModelsAsync(e.SessionDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in KeyMomentsViewModel:UpdateTimer_UpdateTimerElapsed");
            }
        }

        #endregion

        private async Task LoadModelsAsync(RaceSessionDetails sessionDetails)
        {
            try
            {
                var keyMoments = await _keyMomentsRepository.GetKeyMomentsAsync(
                    (SeriesTypes)sessionDetails.SeriesId,
                    sessionDetails.RaceId,
                    sessionDetails.Year);

                foreach (var keyMoment in keyMoments)
                {
                    var existing = Models.FirstOrDefault(m => m.NoteId == keyMoment.NoteId);

                    if (existing != null)
                    {
                        existing.Comments = keyMoment.Note;
                    }
                    else
                    {
                        Models.Add(new KeyMomentsModel()
                        {
                            NoteId = keyMoment.NoteId,
                            FlagState = (int)keyMoment.FlagState,
                            Lap = keyMoment.LapNumber,
                            Comments = keyMoment.Note,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in KeyMomentsViewModel:LoadModelsAsync");
            }
        }

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _logger = null;
                _keyMomentsRepository = null;
            }
            // free native resources if there are any.
        }


        ~KeyMomentsViewModel()
        {
            Debug.WriteLine("********************************* KeyMomentsViewModel Disposed");
        }
    }
}