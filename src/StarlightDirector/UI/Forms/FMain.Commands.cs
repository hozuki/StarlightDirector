using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Input;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.DirectorApplication;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void RegisterCommands() {
            mnuProjectNew.BindCommand(CmdProjectNew);
            tsbProjectNew.BindCommand(CmdProjectNew);
            mnuProjectOpen.BindCommand(CmdProjectOpen);
            tsbProjectOpen.BindCommand(CmdProjectOpen);
            mnuProjectSave.BindCommand(CmdProjectSave);
            tsbProjectSave.BindCommand(CmdProjectSave);
            mnuProjectSaveAs.BindCommand(CmdProjectSaveAs);
            mnuProjectExit.BindCommand(CmdProjectExit);
            sysClose.BindCommand(CmdProjectExit);

            mnuProjectBeatmapSettings.BindCommand(CmdProjectBeatmapSettings);
            mnuProjectBeatmapStats.BindCommand(CmdProjectBeatmapStats);
            mnuProjectMusicSettings.BindCommand(CmdProjectMusicSettings);

            mnuScoreDifficultyDebut.BindCommand(CmdScoreDifficultySelect, Difficulty.Debut);
            mnuScoreDifficultyRegular.BindCommand(CmdScoreDifficultySelect, Difficulty.Regular);
            mnuScoreDifficultyPro.BindCommand(CmdScoreDifficultySelect, Difficulty.Pro);
            mnuScoreDifficultyMaster.BindCommand(CmdScoreDifficultySelect, Difficulty.Master);
            mnuScoreDifficultyMasterPlus.BindCommand(CmdScoreDifficultySelect, Difficulty.MasterPlus);

            mnuEditUndo.BindCommand(CmdEditUndo);
            mnuEditRedo.BindCommand(CmdEditRedo);
            mnuEditCut.BindCommand(CmdEditCut);
            mnuEditCopy.BindCommand(CmdEditCopy);
            mnuEditPaste.BindCommand(CmdEditPaste);
            tsbEditUndo.BindCommand(CmdEditUndo);
            tsbEditRedo.BindCommand(CmdEditRedo);
            tsbEditCut.BindCommand(CmdEditCut);
            tsbEditCopy.BindCommand(CmdEditCopy);
            tsbEditPaste.BindCommand(CmdEditPaste);

            mnuEditModeSelect.BindCommand(CmdEditModeSet, ScoreEditMode.Select);
            mnuEditModeTap.BindCommand(CmdEditModeSet, ScoreEditMode.Tap);
            mnuEditModeHoldFlick.BindCommand(CmdEditModeSet, ScoreEditMode.HoldFlick);
            mnuEditModeSlide.BindCommand(CmdEditModeSet, ScoreEditMode.Slide);

            mnuEditGoToMeasure.BindCommand(CmdEditGoToMeasure);
            mnuEditGoToTime.BindCommand(CmdEditGoToTime);

            // The ShortcutDisplayStrings are set separately in Form_Load.
            mnuScoreNoteStartPositionAt0.BindCommand(CmdScoreNoteStartPositionSetAt, NotePosition.Default);
            mnuScoreNoteStartPositionAt1.BindCommand(CmdScoreNoteStartPositionSetAt, NotePosition.P1);
            mnuScoreNoteStartPositionAt2.BindCommand(CmdScoreNoteStartPositionSetAt, NotePosition.P2);
            mnuScoreNoteStartPositionAt3.BindCommand(CmdScoreNoteStartPositionSetAt, NotePosition.P3);
            mnuScoreNoteStartPositionAt4.BindCommand(CmdScoreNoteStartPositionSetAt, NotePosition.P4);
            mnuScoreNoteStartPositionAt5.BindCommand(CmdScoreNoteStartPositionSetAt, NotePosition.P5);

            mnuScoreNoteStartPositionMoveLeft.BindCommand(CmdScoreNoteStartPositionMoveLeft);
            mnuScoreNoteStartPositionMoveRight.BindCommand(CmdScoreNoteStartPositionMoveRight);

            mnuScoreNoteStartPositionTo0.BindCommand(CmdScoreNoteStartPositionSetTo, NotePosition.Default);
            mnuScoreNoteStartPositionTo1.BindCommand(CmdScoreNoteStartPositionSetTo, NotePosition.P1);
            mnuScoreNoteStartPositionTo2.BindCommand(CmdScoreNoteStartPositionSetTo, NotePosition.P2);
            mnuScoreNoteStartPositionTo3.BindCommand(CmdScoreNoteStartPositionSetTo, NotePosition.P3);
            mnuScoreNoteStartPositionTo4.BindCommand(CmdScoreNoteStartPositionSetTo, NotePosition.P4);
            mnuScoreNoteStartPositionTo5.BindCommand(CmdScoreNoteStartPositionSetTo, NotePosition.P5);

            mnuScoreNoteDelete.BindCommand(CmdScoreNoteDelete);
            tsbScoreNoteDelete.BindCommand(CmdScoreNoteDelete);
            ctxScoreNoteDelete.BindCommand(CmdScoreNoteDelete);
            mnuScoreNoteResetToTap.BindCommand(CmdScoreNoteResetToTap);
            tsbScoreNoteResetToTap.BindCommand(CmdScoreNoteResetToTap);
            ctxScoreNoteResetToTap.BindCommand(CmdScoreNoteResetToTap);

            mnuScoreNoteInsertSpecial.BindCommand(CmdScoreNoteInsertSpecial);
            tsbScoreNoteInsertSpecial.BindCommand(CmdScoreNoteInsertSpecial);
            ctxScoreNoteInsertSpecial.BindCommand(CmdScoreNoteInsertSpecial);
            ctxScoreNoteModifySpecial.BindCommand(CmdScoreNoteModifySpecial);
            ctxScoreNoteDeleteSpecial.BindCommand(CmdScoreNoteDeleteSpecial);

            mnuEditSelectAllMeasures.BindCommand(CmdEditSelectAllMeasures);
            mnuEditSelectAllNotes.BindCommand(CmdEditSelectAllNotes);
            // For its key handling, see FMain.OnProcessCmdKey:
            // http://stackoverflow.com/questions/18930318/previewkeydown-not-firing.
            mnuEditSelectClearAll.BindCommand(CmdEditSelectClearAll);

            mnuScoreMeasureAppend.BindCommand(CmdScoreMeasureAppend);
            tsbScoreMeasureAppend.BindCommand(CmdScoreMeasureAppend);
            mnuScoreMeasureAppendMultiple.BindCommand(CmdScoreMeasureAppendMultiple);
            tsbScoreMeasureAppendMultiple.BindCommand(CmdScoreMeasureAppendMultiple);
            mnuScoreMeasureInsert.BindCommand(CmdScoreMeasureInsert);
            tsbScoreMeasureInsert.BindCommand(CmdScoreMeasureInsert);
            mnuScoreMeasureInsertMultiple.BindCommand(CmdScoreMeasureInsertMultiple);
            tsbScoreMeasureInsertMultiple.BindCommand(CmdScoreMeasureInsertMultiple);
            mnuScoreMeasureDelete.BindCommand(CmdScoreMeasureDelete);
            tsbScoreMeasureDelete.BindCommand(CmdScoreMeasureDelete);
            ctxScoreMeasureDelete.BindCommand(CmdScoreMeasureDelete);

            mnuViewZoomIn.BindCommand(CmdViewZoomIn);
            tsbViewZoomIn.BindCommand(CmdViewZoomIn);
            mnuViewZoomOut.BindCommand(CmdViewZoomOut);
            tsbViewZoomOut.BindCommand(CmdViewZoomOut);

            mnuViewZoomToBeat1O4.BindCommand(CmdViewZoomToBeat, 4);
            mnuViewZoomToBeat1O6.BindCommand(CmdViewZoomToBeat, 6);
            mnuViewZoomToBeat1O8.BindCommand(CmdViewZoomToBeat, 8);
            mnuViewZoomToBeat1O12.BindCommand(CmdViewZoomToBeat, 12);
            mnuViewZoomToBeat1O16.BindCommand(CmdViewZoomToBeat, 16);
            mnuViewZoomToBeat1O24.BindCommand(CmdViewZoomToBeat, 24);
            mnuViewZoomToBeat1O32.BindCommand(CmdViewZoomToBeat, 32);
            mnuViewZoomToBeat1O48.BindCommand(CmdViewZoomToBeat, 48);
            mnuViewZoomToBeat1O96.BindCommand(CmdViewZoomToBeat, 96);

            mnuViewHighlightModeFourBeats.BindCommand(CmdViewHighlightModeSet, PrimaryBeatMode.EveryFourBeats);
            mnuViewHighlightModeThreeBeats.BindCommand(CmdViewHighlightModeSet, PrimaryBeatMode.EveryThreeBeats);

            mnuPreviewFromThisMeasure.BindCommand(CmdPreviewFromThisMeasure);
            tsbPreviewFromThisMeasure.BindCommand(CmdPreviewFromThisMeasure);
            mnuPreviewFromStart.BindCommand(CmdPreviewFromStart);
            tsbPreviewFromStart.BindCommand(CmdPreviewFromStart);
            mnuPreviewStop.BindCommand(CmdPreviewStop);
            tsbPreviewStop.BindCommand(CmdPreviewStop);

            mnuToolsExportCsv.BindCommand(CmdToolsExportCsv);
            mnuToolsExportTxt.BindCommand(CmdToolsExportTxt);
            mnuToolsBuildBdb.BindCommand(CmdToolsBuildBdb);
            tsbToolsBuildBdb.BindCommand(CmdToolsBuildBdb);
            mnuToolsBuildAcb.BindCommand(CmdToolsBuildAcb);
            tsbToolsBuildAcb.BindCommand(CmdToolsBuildAcb);
            mnuToolsSettings.BindCommand(CmdToolsSettings);

            mnuHelpAbout.BindCommand(CmdHelpAbout);

            tsbToolsTestReload.BindCommand(CmdToolsTestReload);
            tsbToolsTestLaunch.BindCommand(CmdToolsTestLaunch);
            tsbToolsTestRemotePreviewFromStart.BindCommand(CmdToolsTestRemotePreviewFromStart);
            tsbToolsTestRemotePreviewStop.BindCommand(CmdToolsTestRemotePreviewStop);

            CommandManager.Instance.HookForm(this);
            RegisterCommandEvents(this);
        }

        private static void RegisterCommandEvents([NotNull] Form form) {
            var formType = form.GetType();
            var commandBindingFields = SearchPrivateCommandBindingFields(form);

            foreach (var field in commandBindingFields) {
                if (!(field.GetValue(form) is CommandBinding binding)) {
                    continue;
                }

                var commandName = field.Name;

                SubscribeEvent(binding, commandName, "Executed", typeof(EventHandler<ExecutedEventArgs>), true);
                SubscribeEvent(binding, commandName, "Reverted", typeof(EventHandler<RevertedEventArgs>));
                SubscribeEvent(binding, commandName, "QueryCanExecute", typeof(EventHandler<QueryCanExecuteEventArgs>));
                SubscribeEvent(binding, commandName, "QueryCanRevert", typeof(EventHandler<QueryCanRevertEventArgs>));
                SubscribeEvent(binding, commandName, "QueryCanRecord", typeof(EventHandler<QueryCanRecordEventArgs>));
            }

            void SubscribeEvent(CommandBinding binding, string commandName, string eventName, Type handlerType, bool warnIfErrored = false) {
                var handlerName = commandName + "_" + eventName;
                MethodInfo handlerMethod;

                try {
                    handlerMethod = formType.GetMethod(handlerName, ReflectionBindings.PrivateInstance);
                } catch (AmbiguousMatchException) {
                    if (warnIfErrored) {
                        Debug.Print($"Warning: multiple instances of handler method '{handlerName}' found in class '{formType.Name}'. Ignoring all.");
                    }

                    return;
                }

                if (handlerMethod == null) {
                    if (warnIfErrored) {
                        Debug.Print($"Warning: required instance handler method '{handlerName}' is not found in class '{formType.Name}'.");
                    }

                    return;
                }

                var cmdType = binding.GetType();
                var eventField = cmdType.GetEvent(eventName, ReflectionBindings.PrivateInstance | ReflectionBindings.PublicInstance);

                if (eventField == null) {
                    return;
                }

                var methodDelegate = Delegate.CreateDelegate(handlerType, form, handlerMethod);

                eventField.AddEventHandler(binding, methodDelegate);
            }
        }

    }
}
