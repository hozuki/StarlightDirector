using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void RegisterCommands() {
            mnuFileNew.Attach(CmdFileNew);
            tsbFileNew.Attach(CmdFileNew);
            mnuFileOpen.Attach(CmdFileOpen);
            tsbFileOpen.Attach(CmdFileOpen);
            mnuFileSave.Attach(CmdFileSave);
            tsbFileSave.Attach(CmdFileSave);
            mnuFileExit.Attach(CmdFileExit);
            sysClose.Attach(CmdFileExit);

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

            mnuEditSelectAllMeasures.Attach(CmdEditSelectAllMeasures);
            mnuEditSelectAllNotes.Attach(CmdEditSelectAllNotes);
            mnuEditMeasureAppendMany.Attach(CmdEditMeasureAppendMany);
            tsbEditMeasureAppendMany.Attach(CmdEditMeasureAppendMany);
            mnuEditMeasureAppend.Attach(CmdEditMeasureAppend);
            tsbEditMeasureAppend.Attach(CmdEditMeasureAppend);
            mnuEditMeasureDelete.Attach(CmdEditMeasureDelete);
            tsbEditMeasureDelete.Attach(CmdEditMeasureDelete);
            mnuEditNoteDelete.Attach(CmdEditNoteDelete);
            tsbEditNoteDelete.Attach(CmdEditNoteDelete);

            mnuViewZoomIn.Attach(CmdViewZoomIn);
            mnuViewZoomOut.Attach(CmdViewZoomOut);

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
                SubscribeEvent(commandObject, commandName, "Executed", typeof(ExecutedEventHandler), true);
                SubscribeEvent(commandObject, commandName, "Reverted", typeof(RevertedEventHandler));
                SubscribeEvent(commandObject, commandName, "QueryCanExecute", typeof(QueryCanExecuteEventHandler));
                SubscribeEvent(commandObject, commandName, "QueryCanRevert", typeof(QueryCanRevertEventHandler));
                SubscribeEvent(commandObject, commandName, "QueryRecordToHistory", typeof(QueryRecordToHistoryEventHandler));
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
                        Debug.Print($"Warning: required instance handler method '{handlerName}' is not found in this class.");
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
