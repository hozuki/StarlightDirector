using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls.Editing;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void RegisterCommands() {
            mnuProjectNew.Attach(CmdProjectNew);
            tsbProjectNew.Attach(CmdProjectNew);
            mnuProjectOpen.Attach(CmdProjectOpen);
            tsbProjectOpen.Attach(CmdProjectOpen);
            mnuProjectSave.Attach(CmdProjectSave);
            tsbProjectSave.Attach(CmdProjectSave);
            mnuProjecgtSaveAs.Attach(CmdProjectSaveAs);
            mnuProjectExit.Attach(CmdProjectExit);
            sysClose.Attach(CmdProjectExit);

            mnuProjectBeatmapSettings.Attach(CmdProjectBeatmapSettings);
            mnuProjectBeatmapStats.Attach(CmdProjectBeatmapStats);
            mnuProjectMusicSettings.Attach(CmdProjectMusicSettings);

            mnuScoreDifficultyDebut.SetParameter(Difficulty.Debut);
            mnuScoreDifficultyRegular.SetParameter(Difficulty.Regular);
            mnuScoreDifficultyPro.SetParameter(Difficulty.Pro);
            mnuScoreDifficultyMaster.SetParameter(Difficulty.Master);
            mnuScoreDifficultyMasterPlus.SetParameter(Difficulty.MasterPlus);
            mnuScoreDifficultyDebut.Attach(CmdScoreDifficultySelect);
            mnuScoreDifficultyRegular.Attach(CmdScoreDifficultySelect);
            mnuScoreDifficultyPro.Attach(CmdScoreDifficultySelect);
            mnuScoreDifficultyMaster.Attach(CmdScoreDifficultySelect);
            mnuScoreDifficultyMasterPlus.Attach(CmdScoreDifficultySelect);

            mnuEditUndo.Attach(CmdEditUndo);
            mnuEditRedo.Attach(CmdEditRedo);
            mnuEditCut.Attach(CmdEditCut);
            mnuEditCopy.Attach(CmdEditCopy);
            mnuEditPaste.Attach(CmdEditPaste);
            tsbEditUndo.Attach(CmdEditUndo);
            tsbEditRedo.Attach(CmdEditRedo);
            tsbEditCut.Attach(CmdEditCut);
            tsbEditCopy.Attach(CmdEditCopy);
            tsbEditPaste.Attach(CmdEditPaste);

            mnuEditModeSelect.SetParameter(ScoreEditMode.Select);
            mnuEditModeTap.SetParameter(ScoreEditMode.Tap);
            mnuEditModeHoldFlick.SetParameter(ScoreEditMode.HoldFlick);
            mnuEditModeSlide.SetParameter(ScoreEditMode.Slide);
            mnuEditModeSelect.Attach(CmdEditModeSelect);
            mnuEditModeTap.Attach(CmdEditModeTap);
            mnuEditModeHoldFlick.Attach(CmdEditModeHoldFlick);
            mnuEditModeSlide.Attach(CmdEditModeSlide);

            mnuEditGoToMeasure.Attach(CmdEditGoToMeasure);
            mnuEditGoToTime.Attach(CmdEditGoToTime);

            // The ShortcutDisplayStrings are set separately in Form_Load.
            mnuScoreNoteStartPositionAt0.SetParameter(NotePosition.Default);
            mnuScoreNoteStartPositionAt1.SetParameter(NotePosition.Left);
            mnuScoreNoteStartPositionAt2.SetParameter(NotePosition.CenterLeft);
            mnuScoreNoteStartPositionAt3.SetParameter(NotePosition.Center);
            mnuScoreNoteStartPositionAt4.SetParameter(NotePosition.CenterRight);
            mnuScoreNoteStartPositionAt5.SetParameter(NotePosition.Right);
            mnuScoreNoteStartPositionAt0.Attach(CmdScoreNoteStartPositionAt0);
            mnuScoreNoteStartPositionAt1.Attach(CmdScoreNoteStartPositionAt1);
            mnuScoreNoteStartPositionAt2.Attach(CmdScoreNoteStartPositionAt2);
            mnuScoreNoteStartPositionAt3.Attach(CmdScoreNoteStartPositionAt3);
            mnuScoreNoteStartPositionAt4.Attach(CmdScoreNoteStartPositionAt4);
            mnuScoreNoteStartPositionAt5.Attach(CmdScoreNoteStartPositionAt5);

            mnuScoreNoteStartPositionMoveLeft.Attach(CmdScoreNoteStartPositionMoveLeft);
            mnuScoreNoteStartPositionMoveRight.Attach(CmdScoreNoteStartPositionMoveRight);

            mnuScoreNoteStartPositionTo0.SetParameter(NotePosition.Default);
            mnuScoreNoteStartPositionTo1.SetParameter(NotePosition.Left);
            mnuScoreNoteStartPositionTo2.SetParameter(NotePosition.CenterLeft);
            mnuScoreNoteStartPositionTo3.SetParameter(NotePosition.Center);
            mnuScoreNoteStartPositionTo4.SetParameter(NotePosition.CenterRight);
            mnuScoreNoteStartPositionTo5.SetParameter(NotePosition.Right);
            mnuScoreNoteStartPositionTo0.Attach(CmdScoreNoteStartPositionTo0);
            mnuScoreNoteStartPositionTo1.Attach(CmdScoreNoteStartPositionTo1);
            mnuScoreNoteStartPositionTo2.Attach(CmdScoreNoteStartPositionTo2);
            mnuScoreNoteStartPositionTo3.Attach(CmdScoreNoteStartPositionTo3);
            mnuScoreNoteStartPositionTo4.Attach(CmdScoreNoteStartPositionTo4);
            mnuScoreNoteStartPositionTo5.Attach(CmdScoreNoteStartPositionTo5);

            mnuScoreNoteDelete.Attach(CmdScoreNoteDelete);
            tsbScoreNoteDelete.Attach(CmdScoreNoteDelete);
            ctxScoreNoteDelete.Attach(CmdScoreNoteDelete);
            mnuScoreNoteResetToTap.Attach(CmdScoreNoteResetToTap);
            tsbScoreNoteResetToTap.Attach(CmdScoreNoteResetToTap);
            ctxScoreNoteResetToTap.Attach(CmdScoreNoteResetToTap);

            mnuScoreNoteInsertSpecial.Attach(CmdScoreNoteInsertSpecial);
            tsbScoreNoteInsertSpecial.Attach(CmdScoreNoteInsertSpecial);
            ctxScoreNoteInsertSpecial.Attach(CmdScoreNoteInsertSpecial);
            ctxScoreNoteModifySpecial.Attach(CmdScoreNoteModifySpecial);
            ctxScoreNoteDeleteSpecial.Attach(CmdScoreNoteDeleteSpecial);

            mnuEditSelectAllMeasures.Attach(CmdEditSelectAllMeasures);
            mnuEditSelectAllNotes.Attach(CmdEditSelectAllNotes);
            // For its key handling, see FMain.OnProcessCmdKey: http://stackoverflow.com/questions/18930318/previewkeydown-not-firing.
            mnuEditSelectClearAll.Attach(CmdEditSelectClearAll);

            mnuScoreMeasureAppendMultiple.Attach(CmdScoreMeasureAppendMultiple);
            tsbScoreMeasureAppendMultiple.Attach(CmdScoreMeasureAppendMultiple);
            mnuScoreMeasureAppend.Attach(CmdScoreMeasureAppend);
            tsbScoreMeasureAppend.Attach(CmdScoreMeasureAppend);
            mnuScoreMeasureDelete.Attach(CmdScoreMeasureDelete);
            tsbScoreMeasureDelete.Attach(CmdScoreMeasureDelete);
            tsbScoreMeasureDelete.Attach(CmdScoreMeasureDelete);

            mnuViewZoomIn.Attach(CmdViewZoomIn);
            tsbViewZoomIn.Attach(CmdViewZoomIn);
            mnuViewZoomOut.Attach(CmdViewZoomOut);
            tsbViewZoomOut.Attach(CmdViewZoomOut);

            mnuViewZoomToBeat1O4.SetParameter(4);
            mnuViewZoomToBeat1O6.SetParameter(6);
            mnuViewZoomToBeat1O8.SetParameter(8);
            mnuViewZoomToBeat1O12.SetParameter(12);
            mnuViewZoomToBeat1O16.SetParameter(16);
            mnuViewZoomToBeat1O24.SetParameter(24);
            mnuViewZoomToBeat1O32.SetParameter(32);
            mnuViewZoomToBeat1O48.SetParameter(48);
            mnuViewZoomToBeat1O96.SetParameter(96);
            mnuViewZoomToBeat1O4.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O6.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O8.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O12.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O16.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O24.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O32.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O48.Attach(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O96.Attach(CmdViewZoomToBeat);

            mnuViewHighlightModeFourBeats.SetParameter(PrimaryBeatMode.EveryFourBeats);
            mnuViewHighlightModeThreeBeats.SetParameter(PrimaryBeatMode.EveryThreeBeats);
            mnuViewHighlightModeFourBeats.Attach(CmdViewHighlightModeSet);
            mnuViewHighlightModeThreeBeats.Attach(CmdViewHighlightModeSet);

            mnuPreviewFromThisMeasure.Attach(CmdPreviewFromThisMeasure);
            mnuPreviewFromStart.Attach(CmdPreviewFromStart);
            tsbPreviewFromStart.Attach(CmdPreviewFromStart);
            mnuPreviewStop.Attach(CmdPreviewStop);
            tsbPreviewStop.Attach(CmdPreviewStop);

            mnuToolsExportCsv.Attach(CmdToolsExportCsv);
            mnuToolsBuildBdb.Attach(CmdToolsBuildBdb);
            tsbToolsBuildBdb.Attach(CmdToolsBuildBdb);
            mnuToolsBuildAcb.Attach(CmdToolsBuildAcb);
            tsbToolsBuildAcb.Attach(CmdToolsBuildAcb);
            mnuToolsSettings.Attach(CmdToolsSettings);

            mnuHelpAbout.Attach(CmdHelpAbout);

            CommandManager.HookForm(this);
            RegisterCommandEvents(this);
        }

        private void UnregisterCommands() {
            CommandManager.UnhookForm(this);
        }

        private static void RegisterCommandEvents(Form form) {
            var formType = form.GetType();
            var commandType = typeof(Command);
            const BindingFlags privateInstance = BindingFlags.Instance | BindingFlags.NonPublic;
            const BindingFlags publicInstance = BindingFlags.Instance | BindingFlags.Public;
            var commandFields = formType.GetFields(privateInstance)
                .Where(field => field.FieldType == commandType || field.FieldType.IsSubclassOf(commandType));
            foreach (var field in commandFields) {
                var commandObject = field.GetValue(form) as Command;
                if (commandObject == null) {
                    continue;
                }
                var commandName = field.Name;
                SubscribeEvent(commandObject, commandName, "Executed", typeof(EventHandler<ExecutedEventArgs>), true);
                SubscribeEvent(commandObject, commandName, "Reverted", typeof(EventHandler<RevertedEventArgs>));
                SubscribeEvent(commandObject, commandName, "QueryCanExecute", typeof(EventHandler<QueryCanExecuteEventArgs>));
                SubscribeEvent(commandObject, commandName, "QueryCanRevert", typeof(EventHandler<QueryCanRevertEventArgs>));
                SubscribeEvent(commandObject, commandName, "QueryRecordToHistory", typeof(EventHandler<QueryRecordToHistoryEventArgs>));
            }

            void SubscribeEvent(Command commandObject, string commandName, string eventName, Type handlerType, bool warnIfNotFound = false)
            {
                var handlerName = commandName + "_" + eventName;
                MethodInfo handlerMethod;
                try {
                    handlerMethod = formType.GetMethod(handlerName, privateInstance);
                } catch (AmbiguousMatchException) {
                    return;
                }
                if (handlerMethod == null) {
                    if (warnIfNotFound) {
                        Debug.Print($"Warning: required instance handler method '{handlerName}' is not found in class '{formType.Name}'.");
                    }
                    return;
                }
                var cmdType = commandObject.GetType();
                var eventField = cmdType.GetEvent(eventName, publicInstance | privateInstance);
                if (eventField == null) {
                    return;
                }
                var methodDelegate = Delegate.CreateDelegate(handlerType, form, handlerMethod);
                eventField.AddEventHandler(commandObject, methodDelegate);
            }
        }

    }
}
