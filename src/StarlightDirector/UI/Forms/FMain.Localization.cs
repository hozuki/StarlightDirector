using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(mnuProject, "ui.fmain.menu.project.text");
            manager.ApplyText(mnuProjectNew, "ui.fmain.menu.project.new.text");
            manager.ApplyText(mnuProjectOpen, "ui.fmain.menu.project.open.text");
            manager.ApplyText(mnuProjectSave, "ui.fmain.menu.project.save.text");
            manager.ApplyText(mnuProjectSaveAs, "ui.fmain.menu.project.saveas.text");
            manager.ApplyText(mnuProjectBeatmap, "ui.fmain.menu.project.beatmap.text");
            manager.ApplyText(mnuProjectBeatmapStats, "ui.fmain.menu.project.beatmap.stats.text");
            manager.ApplyText(mnuProjectBeatmapSettings, "ui.fmain.menu.project.beatmap.settings.text");
            manager.ApplyText(mnuProjectMusicSettings, "ui.fmain.menu.project.music.settings.text");
            manager.ApplyText(mnuProjectMusic, "ui.fmain.menu.project.music.text");
            manager.ApplyText(mnuProjectExit, "ui.fmain.menu.project.exit.text");

            manager.ApplyText(mnuEdit, "ui.fmain.menu.edit.text");
            manager.ApplyText(mnuEditUndo, "ui.fmain.menu.edit.undo.text");
            manager.ApplyText(mnuEditRedo, "ui.fmain.menu.edit.redo.text");
            manager.ApplyText(mnuEditCut, "ui.fmain.menu.edit.cut.text");
            manager.ApplyText(mnuEditCopy, "ui.fmain.menu.edit.copy.text");
            manager.ApplyText(mnuEditPaste, "ui.fmain.menu.edit.paste.text");
            manager.ApplyText(mnuEditSelect, "ui.fmain.menu.edit.select.text");
            manager.ApplyText(mnuEditSelectAllNotes, "ui.fmain.menu.edit.select.all_notes.text");
            manager.ApplyText(mnuEditSelectAllMeasures, "ui.fmain.menu.edit.select.all_measures.text");
            manager.ApplyText(mnuEditSelectClearAll, "ui.fmain.menu.edit.select.clear_all.text");
            manager.ApplyText(mnuEditMode, "ui.fmain.menu.edit.mode.text");
            manager.ApplyText(mnuEditModeSelect, "ui.fmain.menu.edit.mode.select.text");
            manager.ApplyText(mnuEditModeTap, "ui.fmain.menu.edit.mode.tap.text");
            manager.ApplyText(mnuEditModeHoldFlick, "ui.fmain.menu.edit.mode.hold_flick.text");
            manager.ApplyText(mnuEditModeSlide, "ui.fmain.menu.edit.mode.slide.text");
            manager.ApplyText(mnuEditGoTo, "ui.fmain.menu.edit.goto.text");
            manager.ApplyText(mnuEditGoToMeasure, "ui.fmain.menu.edit.goto.measure.text");
            manager.ApplyText(mnuEditGoToTime, "ui.fmain.menu.edit.goto.time.text");

            manager.ApplyText(mnuView, "ui.fmain.menu.view.text");
            manager.ApplyText(mnuViewZoom, "ui.fmain.menu.view.zoom.text");
            manager.ApplyText(mnuViewZoomIn, "ui.fmain.menu.view.zoom.in.text");
            manager.ApplyText(mnuViewZoomOut, "ui.fmain.menu.view.zoom.out.text");
            manager.ApplyText(mnuViewZoomToBeat, "ui.fmain.menu.view.zoom.to_beat.text");
            manager.ApplyText(mnuViewHighlightMode, "ui.fmain.menu.view.highlight_mode.text");
            manager.ApplyText(mnuViewHighlightModeFourBeats, "ui.fmain.menu.view.highlight_mode.four_beats.text");
            manager.ApplyText(mnuViewHighlightModeThreeBeats, "ui.fmain.menu.view.highlight_mode.three_beats.text");

            manager.ApplyText(mnuScore, "ui.fmain.menu.score.text");
            manager.ApplyText(mnuScoreDifficulty, "ui.fmain.menu.score.difficulty.text");
            manager.ApplyText(mnuScoreDifficultyDebut, "ui.fmain.menu.score.difficulty.debut.text");
            manager.ApplyText(mnuScoreDifficultyRegular, "ui.fmain.menu.score.difficulty.regular.text");
            manager.ApplyText(mnuScoreDifficultyPro, "ui.fmain.menu.score.difficulty.pro.text");
            manager.ApplyText(mnuScoreDifficultyMaster, "ui.fmain.menu.score.difficulty.master.text");
            manager.ApplyText(mnuScoreDifficultyMasterPlus, "ui.fmain.menu.score.difficulty.master_plus.text");
            manager.ApplyText(mnuScoreMeasure, "ui.fmain.menu.score.measure.text");
            manager.ApplyText(mnuScoreMeasureAppend, "ui.fmain.menu.score.measure.append.text");
            manager.ApplyText(mnuScoreMeasureAppendMultiple, "ui.fmain.menu.score.measure.append_multiple.text");
            manager.ApplyText(mnuScoreMeasureInsert, "ui.fmain.menu.score.measure.insert.text");
            manager.ApplyText(mnuScoreMeasureInsertMultiple, "ui.fmain.menu.score.measure.insert_multiple.text");
            manager.ApplyText(mnuScoreMeasureDelete, "ui.fmain.menu.score.measure.delete.text");
            manager.ApplyText(mnuScoreNote, "ui.fmain.menu.score.note.text");
            manager.ApplyText(mnuScoreNoteResetToTap, "ui.fmain.menu.score.note.reset_to_tap.text");
            manager.ApplyText(mnuScoreNoteDelete, "ui.fmain.menu.score.note.delete.text");
            manager.ApplyText(mnuScoreNoteStartPosition, "ui.fmain.menu.score.note.start_position.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt0, "ui.fmain.menu.score.note.start_position.at.default.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt1, "ui.fmain.menu.score.note.start_position.at.1.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt2, "ui.fmain.menu.score.note.start_position.at.2.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt3, "ui.fmain.menu.score.note.start_position.at.3.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt4, "ui.fmain.menu.score.note.start_position.at.4.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt5, "ui.fmain.menu.score.note.start_position.at.5.text");
            manager.ApplyText(mnuScoreNoteStartPositionMoveLeft, "ui.fmain.menu.score.note.start_position.move.left.text");
            manager.ApplyText(mnuScoreNoteStartPositionMoveRight, "ui.fmain.menu.score.note.start_position.move.right.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo0, "ui.fmain.menu.score.note.start_position.to.default.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo1, "ui.fmain.menu.score.note.start_position.to.1.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo2, "ui.fmain.menu.score.note.start_position.to.2.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo3, "ui.fmain.menu.score.note.start_position.to.3.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo4, "ui.fmain.menu.score.note.start_position.to.4.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo5, "ui.fmain.menu.score.note.start_position.to.5.text");
            manager.ApplyText(mnuScoreNoteInsertSpecial, "ui.fmain.menu.score.note.insert_special.text");

            manager.ApplyText(mnuPreview, "ui.fmain.menu.preview.text");
            manager.ApplyText(mnuPreviewFromThisMeasure, "ui.fmain.menu.preview.from_this_measure.text");
            manager.ApplyText(mnuPreviewFromStart, "ui.fmain.menu.preview.from_start.text");
            manager.ApplyText(mnuPreviewStop, "ui.fmain.menu.preview.stop.text");

            manager.ApplyText(mnuTools, "ui.fmain.menu.tools.text");
            manager.ApplyText(mnuToolsExport, "ui.fmain.menu.tools.export.text");
            manager.ApplyText(mnuToolsExportCsv, "ui.fmain.menu.tools.export.csv.text");
            manager.ApplyText(mnuToolsExportTxt, "ui.fmain.menu.tools.export.txt.text");
            manager.ApplyText(mnuToolsImport, "ui.fmain.menu.tools.import.text");
            manager.ApplyText(mnuToolsBuild, "ui.fmain.menu.tools.build.text");
            manager.ApplyText(mnuToolsBuildBdb, "ui.fmain.menu.tools.build.bdb.text");
            manager.ApplyText(mnuToolsBuildAcb, "ui.fmain.menu.tools.build.acb.text");
            manager.ApplyText(mnuToolsSettings, "ui.fmain.menu.tools.settings.text");

            manager.ApplyText(mnuHelp, "ui.fmain.menu.help.text");
            manager.ApplyText(mnuHelpAbout, "ui.fmain.menu.help.about.text");

            manager.ApplyText(gbStandard, "ui.fmain.frame.standard.text");
            manager.ApplyText(gbEditView, "ui.fmain.frame.edit_view.text");
            manager.ApplyText(gbNote, "ui.fmain.frame.note.text");
            manager.ApplyText(gbMeasure, "ui.fmain.frame.measure.text");
            manager.ApplyText(gbPostprocessing, "ui.fmain.frame.postprocessing.text");

            manager.ApplyText(tsbProjectNew, "ui.fmain.toolbar.standard.new.text");
            manager.ApplyText(tsbProjectOpen, "ui.fmain.toolbar.standard.open.text");
            manager.ApplyText(tsbProjectSave, "ui.fmain.toolbar.standard.save.text");
            manager.ApplyText(tsbEditUndo, "ui.fmain.toolbar.standard.undo.text");
            manager.ApplyText(tsbEditRedo, "ui.fmain.toolbar.standard.redo.text");
            manager.ApplyText(tsbEditCut, "ui.fmain.toolbar.standard.cut.text");
            manager.ApplyText(tsbEditCopy, "ui.fmain.toolbar.standard.copy.text");
            manager.ApplyText(tsbEditPaste, "ui.fmain.toolbar.standard.paste.text");

            manager.ApplyText(tsbEditMode, "ui.fmain.toolbar.edit_view.mode.text");
            manager.ApplyText(toolStripLabel1, "ui.fmain.toolbar.edit_view.difficulty_label.text");
            manager.ApplyText(tsbViewZoomIn, "ui.fmain.toolbar.edit_view.zoom_in.text");
            manager.ApplyText(tsbViewZoomOut, "ui.fmain.toolbar.edit_view.zoom_out.text");

            manager.ApplyText(tsbScoreNoteResetToTap, "ui.fmain.toolbar.note.reset_to_tap.text");
            manager.ApplyText(tsbScoreNoteDelete, "ui.fmain.toolbar.note.delete.text");
            manager.ApplyText(toolStripLabel2, "ui.fmain.toolbar.note.start_position_label.text");
            manager.ApplyText(tsbScoreNoteInsertSpecial, "ui.fmain.toolbar.note.insert_special.text");

            manager.ApplyText(tsbScoreMeasureAppend, "ui.fmain.toolbar.measure.append.text");
            manager.ApplyText(tsbScoreMeasureAppendMultiple, "ui.fmain.toolbar.measure.append_multiple.text");
            manager.ApplyText(tsbScoreMeasureInsert, "ui.fmain.toolbar.measure.insert.text");
            manager.ApplyText(tsbScoreMeasureInsertMultiple, "ui.fmain.toolbar.measure.insert_multiple.text");
            manager.ApplyText(tsbScoreMeasureDelete, "ui.fmain.toolbar.measure.delete.text");

            manager.ApplyText(tsbPreviewFromThisMeasure, "ui.fmain.toolbar.postprocessing.preview.from_this_measure.text");
            manager.ApplyText(tsbPreviewFromStart, "ui.fmain.toolbar.postprocessing.preview.from_start.text");
            manager.ApplyText(tsbPreviewStop, "ui.fmain.toolbar.postprocessing.preview.stop.text");
            manager.ApplyText(tsbToolsBuildBdb, "ui.fmain.toolbar.postprocessing.build.bdb.text");
            manager.ApplyText(tsbToolsBuildAcb, "ui.fmain.toolbar.postprocessing.build.acb.text");

            manager.ApplyText(ctxEditCut, "ui.fmain.context.edit.cut");
            manager.ApplyText(ctxEditCopy, "ui.fmain.context.edit.copy");
            manager.ApplyText(ctxEditPaste, "ui.fmain.context.edit.paste");
            manager.ApplyText(ctxScoreNoteResetToTap, "ui.fmain.context.note.reset_to_tap");
            manager.ApplyText(ctxScoreNoteDelete, "ui.fmain.context.note.delete");
            manager.ApplyText(ctxScoreMeasureDelete, "ui.fmain.context.measure.delete");
            manager.ApplyText(ctxScoreNoteInsertSpecial, "ui.fmain.context.note.insert_special");
            manager.ApplyText(ctxScoreNoteModifySpecial, "ui.fmain.context.note.modify_special");
            manager.ApplyText(ctxScoreNoteDeleteSpecial, "ui.fmain.context.note.delete_special");

            // FMain as a special form, needs an extra title setting.
            UpdateUIIndications();

            CommandHelper.UpdateAllSourceText();

            // Right now the key is definitely released, just set the start position.
            CmdScoreNoteStartPositionSetAt.Command.Execute(NotePosition.Default);
        }

    }
}
