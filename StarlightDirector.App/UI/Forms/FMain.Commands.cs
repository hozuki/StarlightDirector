using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void RegisterCommands() {
            mnuFileNew.Attach(CmdFileNew);
            tsbFileNew.Attach(CmdFileNew);
            mnuFileOpen.Attach(CmdFileOpen);
            tsbFileOpen.Attach(CmdFileOpen);
            mnuFileSave.Attach(CmdFileSave);
            tsbFileSave.Attach(CmdFileSave);
            mnuFileSaveAs.Attach(CmdFileSaveAs);
            mnuFileExit.Attach(CmdFileExit);
            sysClose.Attach(CmdFileExit);

            mnuEditBeatmapSettings.Attach(CmdEditBeatmapSettings);

            mnuEditDifficultyDebut.SetParameter(Difficulty.Debut);
            mnuEditDifficultyRegular.SetParameter(Difficulty.Regular);
            mnuEditDifficultyPro.SetParameter(Difficulty.Pro);
            mnuEditDifficultyMaster.SetParameter(Difficulty.Master);
            mnuEditDifficultyMasterPlus.SetParameter(Difficulty.MasterPlus);
            mnuEditDifficultyDebut.Attach(CmdEditDifficultySelect);
            mnuEditDifficultyRegular.Attach(CmdEditDifficultySelect);
            mnuEditDifficultyPro.Attach(CmdEditDifficultySelect);
            mnuEditDifficultyMaster.Attach(CmdEditDifficultySelect);
            mnuEditDifficultyMasterPlus.Attach(CmdEditDifficultySelect);

            mnuEditModeSelect.SetParameter(ScoreEditMode.Select);
            mnuEditModeTap.SetParameter(ScoreEditMode.Tap);
            mnuEditModeHold.SetParameter(ScoreEditMode.Hold);
            mnuEditModeFlick.SetParameter(ScoreEditMode.Flick);
            mnuEditModeSlide.SetParameter(ScoreEditMode.Slide);
            mnuEditModeSelect.Attach(CmdEditModeSelect);
            mnuEditModeTap.Attach(CmdEditModeSelect);
            mnuEditModeHold.Attach(CmdEditModeSelect);
            mnuEditModeFlick.Attach(CmdEditModeSelect);
            mnuEditModeSlide.Attach(CmdEditModeSelect);

            mnuEditNoteStartPosition1.SetParameter(NotePosition.Left);
            mnuEditNoteStartPosition2.SetParameter(NotePosition.CenterLeft);
            mnuEditNoteStartPosition3.SetParameter(NotePosition.Center);
            mnuEditNoteStartPosition4.SetParameter(NotePosition.CenterRight);
            mnuEditNoteStartPosition5.SetParameter(NotePosition.Right);
            mnuEditNoteStartPosition1.Attach(CmdEditNoteStartPosition);
            mnuEditNoteStartPosition2.Attach(CmdEditNoteStartPosition);
            mnuEditNoteStartPosition3.Attach(CmdEditNoteStartPosition);
            mnuEditNoteStartPosition4.Attach(CmdEditNoteStartPosition);
            mnuEditNoteStartPosition5.Attach(CmdEditNoteStartPosition);
            tsbEditNoteStartPosition1.SetParameter(NotePosition.Left);
            tsbEditNoteStartPosition2.SetParameter(NotePosition.CenterLeft);
            tsbEditNoteStartPosition3.SetParameter(NotePosition.Center);
            tsbEditNoteStartPosition4.SetParameter(NotePosition.CenterRight);
            tsbEditNoteStartPosition5.SetParameter(NotePosition.Right);
            tsbEditNoteStartPosition1.Attach(CmdEditNoteStartPosition);
            tsbEditNoteStartPosition2.Attach(CmdEditNoteStartPosition);
            tsbEditNoteStartPosition3.Attach(CmdEditNoteStartPosition);
            tsbEditNoteStartPosition4.Attach(CmdEditNoteStartPosition);
            tsbEditNoteStartPosition5.Attach(CmdEditNoteStartPosition);
            mnuEditNoteDelete.Attach(CmdEditNoteDelete);
            tsbEditNoteDelete.Attach(CmdEditNoteDelete);

            mnuEditSelectAllMeasures.Attach(CmdEditSelectAllMeasures);
            mnuEditSelectAllNotes.Attach(CmdEditSelectAllNotes);
            mnuEditMeasureAppendMany.Attach(CmdEditMeasureAppendMany);
            tsbEditMeasureAppendMany.Attach(CmdEditMeasureAppendMany);
            mnuEditMeasureAppend.Attach(CmdEditMeasureAppend);
            tsbEditMeasureAppend.Attach(CmdEditMeasureAppend);
            mnuEditMeasureDelete.Attach(CmdEditMeasureDelete);
            tsbEditMeasureDelete.Attach(CmdEditMeasureDelete);

            mnuViewZoomIn.Attach(CmdViewZoomIn);
            tsbViewZoomIn.Attach(CmdViewZoomIn);
            mnuViewZoomOut.Attach(CmdViewZoomOut);
            tsbViewZoomOut.Attach(CmdViewZoomOut);

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
