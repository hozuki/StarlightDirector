using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Input;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void RegisterCommands() {
            mnuProjectNew.Bind(CmdProjectNew);
            tsbProjectNew.Bind(CmdProjectNew);
            mnuProjectOpen.Bind(CmdProjectOpen);
            tsbProjectOpen.Bind(CmdProjectOpen);
            mnuProjectSave.Bind(CmdProjectSave);
            tsbProjectSave.Bind(CmdProjectSave);
            mnuProjectSaveAs.Bind(CmdProjectSaveAs);
            mnuProjectExit.Bind(CmdProjectExit);
            sysClose.Bind(CmdProjectExit);

            mnuProjectBeatmapSettings.Bind(CmdProjectBeatmapSettings);
            mnuProjectBeatmapStats.Bind(CmdProjectBeatmapStats);
            mnuProjectMusicSettings.Bind(CmdProjectMusicSettings);

            mnuScoreDifficultyDebut.SetParameter(Difficulty.Debut);
            mnuScoreDifficultyRegular.SetParameter(Difficulty.Regular);
            mnuScoreDifficultyPro.SetParameter(Difficulty.Pro);
            mnuScoreDifficultyMaster.SetParameter(Difficulty.Master);
            mnuScoreDifficultyMasterPlus.SetParameter(Difficulty.MasterPlus);
            mnuScoreDifficultyDebut.Bind(CmdScoreDifficultySelect);
            mnuScoreDifficultyRegular.Bind(CmdScoreDifficultySelect);
            mnuScoreDifficultyPro.Bind(CmdScoreDifficultySelect);
            mnuScoreDifficultyMaster.Bind(CmdScoreDifficultySelect);
            mnuScoreDifficultyMasterPlus.Bind(CmdScoreDifficultySelect);

            mnuEditUndo.Bind(CmdEditUndo);
            mnuEditRedo.Bind(CmdEditRedo);
            mnuEditCut.Bind(CmdEditCut);
            mnuEditCopy.Bind(CmdEditCopy);
            mnuEditPaste.Bind(CmdEditPaste);
            tsbEditUndo.Bind(CmdEditUndo);
            tsbEditRedo.Bind(CmdEditRedo);
            tsbEditCut.Bind(CmdEditCut);
            tsbEditCopy.Bind(CmdEditCopy);
            tsbEditPaste.Bind(CmdEditPaste);

            mnuEditModeSelect.SetParameter(ScoreEditMode.Select);
            mnuEditModeTap.SetParameter(ScoreEditMode.Tap);
            mnuEditModeHoldFlick.SetParameter(ScoreEditMode.HoldFlick);
            mnuEditModeSlide.SetParameter(ScoreEditMode.Slide);
            mnuEditModeSelect.Bind(CmdEditModeSelect);
            mnuEditModeTap.Bind(CmdEditModeTap);
            mnuEditModeHoldFlick.Bind(CmdEditModeHoldFlick);
            mnuEditModeSlide.Bind(CmdEditModeSlide);

            mnuEditGoToMeasure.Bind(CmdEditGoToMeasure);
            mnuEditGoToTime.Bind(CmdEditGoToTime);

            // The ShortcutDisplayStrings are set separately in Form_Load.
            mnuScoreNoteStartPositionAt0.SetParameter(NotePosition.Default);
            mnuScoreNoteStartPositionAt1.SetParameter(NotePosition.P1);
            mnuScoreNoteStartPositionAt2.SetParameter(NotePosition.P2);
            mnuScoreNoteStartPositionAt3.SetParameter(NotePosition.P3);
            mnuScoreNoteStartPositionAt4.SetParameter(NotePosition.P4);
            mnuScoreNoteStartPositionAt5.SetParameter(NotePosition.P5);
            mnuScoreNoteStartPositionAt0.Bind(CmdScoreNoteStartPositionAt0);
            mnuScoreNoteStartPositionAt1.Bind(CmdScoreNoteStartPositionAt1);
            mnuScoreNoteStartPositionAt2.Bind(CmdScoreNoteStartPositionAt2);
            mnuScoreNoteStartPositionAt3.Bind(CmdScoreNoteStartPositionAt3);
            mnuScoreNoteStartPositionAt4.Bind(CmdScoreNoteStartPositionAt4);
            mnuScoreNoteStartPositionAt5.Bind(CmdScoreNoteStartPositionAt5);

            mnuScoreNoteStartPositionMoveLeft.Bind(CmdScoreNoteStartPositionMoveLeft);
            mnuScoreNoteStartPositionMoveRight.Bind(CmdScoreNoteStartPositionMoveRight);

            mnuScoreNoteStartPositionTo0.SetParameter(NotePosition.Default);
            mnuScoreNoteStartPositionTo1.SetParameter(NotePosition.P1);
            mnuScoreNoteStartPositionTo2.SetParameter(NotePosition.P2);
            mnuScoreNoteStartPositionTo3.SetParameter(NotePosition.P3);
            mnuScoreNoteStartPositionTo4.SetParameter(NotePosition.P4);
            mnuScoreNoteStartPositionTo5.SetParameter(NotePosition.P5);
            mnuScoreNoteStartPositionTo0.Bind(CmdScoreNoteStartPositionTo0);
            mnuScoreNoteStartPositionTo1.Bind(CmdScoreNoteStartPositionTo1);
            mnuScoreNoteStartPositionTo2.Bind(CmdScoreNoteStartPositionTo2);
            mnuScoreNoteStartPositionTo3.Bind(CmdScoreNoteStartPositionTo3);
            mnuScoreNoteStartPositionTo4.Bind(CmdScoreNoteStartPositionTo4);
            mnuScoreNoteStartPositionTo5.Bind(CmdScoreNoteStartPositionTo5);

            mnuScoreNoteDelete.Bind(CmdScoreNoteDelete);
            tsbScoreNoteDelete.Bind(CmdScoreNoteDelete);
            ctxScoreNoteDelete.Bind(CmdScoreNoteDelete);
            mnuScoreNoteResetToTap.Bind(CmdScoreNoteResetToTap);
            tsbScoreNoteResetToTap.Bind(CmdScoreNoteResetToTap);
            ctxScoreNoteResetToTap.Bind(CmdScoreNoteResetToTap);

            mnuScoreNoteInsertSpecial.Bind(CmdScoreNoteInsertSpecial);
            tsbScoreNoteInsertSpecial.Bind(CmdScoreNoteInsertSpecial);
            ctxScoreNoteInsertSpecial.Bind(CmdScoreNoteInsertSpecial);
            ctxScoreNoteModifySpecial.Bind(CmdScoreNoteModifySpecial);
            ctxScoreNoteDeleteSpecial.Bind(CmdScoreNoteDeleteSpecial);

            mnuEditSelectAllMeasures.Bind(CmdEditSelectAllMeasures);
            mnuEditSelectAllNotes.Bind(CmdEditSelectAllNotes);
            // For its key handling, see FMain.OnProcessCmdKey:
            // http://stackoverflow.com/questions/18930318/previewkeydown-not-firing.
            mnuEditSelectClearAll.Bind(CmdEditSelectClearAll);

            mnuScoreMeasureAppend.Bind(CmdScoreMeasureAppend);
            tsbScoreMeasureAppend.Bind(CmdScoreMeasureAppend);
            mnuScoreMeasureAppendMultiple.Bind(CmdScoreMeasureAppendMultiple);
            tsbScoreMeasureAppendMultiple.Bind(CmdScoreMeasureAppendMultiple);
            mnuScoreMeasureInsert.Bind(CmdScoreMeasureInsert);
            tsbScoreMeasureInsert.Bind(CmdScoreMeasureInsert);
            mnuScoreMeasureInsertMultiple.Bind(CmdScoreMeasureInsertMultiple);
            tsbScoreMeasureInsertMultiple.Bind(CmdScoreMeasureInsertMultiple);
            mnuScoreMeasureDelete.Bind(CmdScoreMeasureDelete);
            tsbScoreMeasureDelete.Bind(CmdScoreMeasureDelete);
            ctxScoreMeasureDelete.Bind(CmdScoreMeasureDelete);

            mnuViewZoomIn.Bind(CmdViewZoomIn);
            tsbViewZoomIn.Bind(CmdViewZoomIn);
            mnuViewZoomOut.Bind(CmdViewZoomOut);
            tsbViewZoomOut.Bind(CmdViewZoomOut);

            mnuViewZoomToBeat1O4.SetParameter(4);
            mnuViewZoomToBeat1O6.SetParameter(6);
            mnuViewZoomToBeat1O8.SetParameter(8);
            mnuViewZoomToBeat1O12.SetParameter(12);
            mnuViewZoomToBeat1O16.SetParameter(16);
            mnuViewZoomToBeat1O24.SetParameter(24);
            mnuViewZoomToBeat1O32.SetParameter(32);
            mnuViewZoomToBeat1O48.SetParameter(48);
            mnuViewZoomToBeat1O96.SetParameter(96);
            mnuViewZoomToBeat1O4.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O6.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O8.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O12.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O16.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O24.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O32.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O48.Bind(CmdViewZoomToBeat);
            mnuViewZoomToBeat1O96.Bind(CmdViewZoomToBeat);

            mnuViewHighlightModeFourBeats.SetParameter(PrimaryBeatMode.EveryFourBeats);
            mnuViewHighlightModeThreeBeats.SetParameter(PrimaryBeatMode.EveryThreeBeats);
            mnuViewHighlightModeFourBeats.Bind(CmdViewHighlightModeSet);
            mnuViewHighlightModeThreeBeats.Bind(CmdViewHighlightModeSet);

            mnuPreviewFromThisMeasure.Bind(CmdPreviewFromThisMeasure);
            tsbPreviewFromThisMeasure.Bind(CmdPreviewFromThisMeasure);
            mnuPreviewFromStart.Bind(CmdPreviewFromStart);
            tsbPreviewFromStart.Bind(CmdPreviewFromStart);
            mnuPreviewStop.Bind(CmdPreviewStop);
            tsbPreviewStop.Bind(CmdPreviewStop);

            mnuToolsExportCsv.Bind(CmdToolsExportCsv);
            mnuToolsBuildBdb.Bind(CmdToolsBuildBdb);
            tsbToolsBuildBdb.Bind(CmdToolsBuildBdb);
            mnuToolsBuildAcb.Bind(CmdToolsBuildAcb);
            tsbToolsBuildAcb.Bind(CmdToolsBuildAcb);
            mnuToolsSettings.Bind(CmdToolsSettings);

            mnuHelpAbout.Bind(CmdHelpAbout);

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
