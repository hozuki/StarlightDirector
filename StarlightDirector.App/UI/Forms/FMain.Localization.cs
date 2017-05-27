using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(mnuProject, "fmain.menu.project.text");
            manager.ApplyText(mnuProjectNew, "fmain.menu.project.new.text");
            manager.ApplyText(mnuProjectOpen, "fmain.menu.project.open.text");
            manager.ApplyText(mnuProjectSave, "fmain.menu.project.save.text");
            manager.ApplyText(mnuProjectSaveAs, "fmain.menu.project.saveas.text");
            manager.ApplyText(mnuProjectBeatmap, "fmain.menu.project.beatmap.text");
            manager.ApplyText(mnuProjectBeatmapStats, "fmain.menu.project.beatmap.stats.text");
            manager.ApplyText(mnuProjectBeatmapSettings, "fmain.menu.project.beatmap.settings.text");
            manager.ApplyText(mnuProjectMusicSettings, "fmain.menu.project.music.settings.text");
            manager.ApplyText(mnuProjectMusic, "fmain.menu.project.music.text");
            manager.ApplyText(mnuProjectExit, "fmain.menu.project.exit.text");

            manager.ApplyText(mnuEdit, "fmain.menu.edit.text");
            manager.ApplyText(mnuEditUndo, "fmain.menu.edit.undo.text");
            manager.ApplyText(mnuEditRedo, "fmain.menu.edit.redo.text");
            manager.ApplyText(mnuEditCut, "fmain.menu.edit.cut.text");
            manager.ApplyText(mnuEditCopy, "fmain.menu.edit.copy.text");
            manager.ApplyText(mnuEditPaste, "fmain.menu.edit.paste.text");
            manager.ApplyText(mnuEditSelect, "fmain.menu.edit.select.text");
            manager.ApplyText(mnuEditSelectAllNotes, "fmain.menu.edit.select.all_notes.text");
            manager.ApplyText(mnuEditSelectAllMeasures, "fmain.menu.edit.select.all_measures.text");
            manager.ApplyText(mnuEditSelectClearAll, "fmain.menu.edit.select.clear_all.text");
            manager.ApplyText(mnuEditMode, "fmain.menu.edit.mode.text");
            manager.ApplyText(mnuEditModeSelect, "fmain.menu.edit.mode.select.text");
            manager.ApplyText(mnuEditModeTap, "fmain.menu.edit.mode.tap.text");
            manager.ApplyText(mnuEditModeHoldFlick, "fmain.menu.edit.mode.hold_flick.text");
            manager.ApplyText(mnuEditModeSlide, "fmain.menu.edit.mode.slide.text");
            manager.ApplyText(mnuEditGoTo, "fmain.menu.edit.goto.text");
            manager.ApplyText(mnuEditGoToMeasure, "fmain.menu.edit.goto.measure.text");
            manager.ApplyText(mnuEditGoToTime, "fmain.menu.edit.goto.time.text");

            manager.ApplyText(mnuView, "fmain.menu.view.text");
            manager.ApplyText(mnuViewZoom, "fmain.menu.view.zoom.text");
            manager.ApplyText(mnuViewZoomIn, "fmain.menu.view.zoom.in.text");
            manager.ApplyText(mnuViewZoomOut, "fmain.menu.view.zoom.out.text");
            manager.ApplyText(mnuViewZoomToBeat, "fmain.menu.view.zoom.to_beat.text");
            manager.ApplyText(mnuViewHighlightMode, "fmain.menu.view.highlight_mode.text");
            manager.ApplyText(mnuViewHighlightModeFourBeats, "fmain.menu.view.highlight_mode.four_beats.text");
            manager.ApplyText(mnuViewHighlightModeThreeBeats, "fmain.menu.view.highlight_mode.three_beats.text");

            manager.ApplyText(mnuScore, "fmain.menu.score.text");
            manager.ApplyText(mnuScoreDifficulty, "fmain.menu.score.difficulty.text");
            manager.ApplyText(mnuScoreDifficultyDebut, "fmain.menu.score.difficulty.debut.text");
            manager.ApplyText(mnuScoreDifficultyRegular, "fmain.menu.score.difficulty.regular.text");
            manager.ApplyText(mnuScoreDifficultyPro, "fmain.menu.score.difficulty.pro.text");
            manager.ApplyText(mnuScoreDifficultyMaster, "fmain.menu.score.difficulty.master.text");
            manager.ApplyText(mnuScoreDifficultyMasterPlus, "fmain.menu.score.difficulty.master_plus.text");
            manager.ApplyText(mnuScoreMeasure, "fmain.menu.score.measure.text");
            manager.ApplyText(mnuScoreMeasureAppend, "fmain.menu.score.measure.append.text");
            manager.ApplyText(mnuScoreMeasureAppendMultiple, "fmain.menu.score.measure.append_multiple.text");
            manager.ApplyText(mnuScoreMeasureInsert, "fmain.menu.score.measure.insert.text");
            manager.ApplyText(mnuScoreMeasureInsertMultiple, "fmain.menu.score.measure.insert_multiple.text");
            manager.ApplyText(mnuScoreMeasureDelete, "fmain.menu.score.measure.delete.text");
            manager.ApplyText(mnuScoreNote, "fmain.menu.score.note.text");
            manager.ApplyText(mnuScoreNoteResetToTap, "fmain.menu.score.note.reset_to_tap.text");
            manager.ApplyText(mnuScoreNoteDelete, "fmain.menu.score.note.delete.text");
            manager.ApplyText(mnuScoreNoteStartPosition, "fmain.menu.score.note.start_position.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt0, "fmain.menu.score.note.start_position.at.default.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt1, "fmain.menu.score.note.start_position.at.1.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt2, "fmain.menu.score.note.start_position.at.2.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt3, "fmain.menu.score.note.start_position.at.3.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt4, "fmain.menu.score.note.start_position.at.4.text");
            manager.ApplyText(mnuScoreNoteStartPositionAt5, "fmain.menu.score.note.start_position.at.5.text");
            manager.ApplyText(mnuScoreNoteStartPositionMoveLeft, "fmain.menu.score.note.start_position.move.left.text");
            manager.ApplyText(mnuScoreNoteStartPositionMoveRight, "fmain.menu.score.note.start_position.move.right.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo0, "fmain.menu.score.note.start_position.to.default.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo1, "fmain.menu.score.note.start_position.to.1.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo2, "fmain.menu.score.note.start_position.to.2.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo3, "fmain.menu.score.note.start_position.to.3.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo4, "fmain.menu.score.note.start_position.to.4.text");
            manager.ApplyText(mnuScoreNoteStartPositionTo5, "fmain.menu.score.note.start_position.to.5.text");
            manager.ApplyText(mnuScoreNoteInsertSpecial, "fmain.menu.score.note.insert_special.text");

            manager.ApplyText(mnuPreview, "fmain.menu.preview.text");
            manager.ApplyText(mnuPreviewFromThisMeasure, "fmain.menu.preview.from_this_measure.text");
            manager.ApplyText(mnuPreviewFromStart, "fmain.menu.preview.from_start.text");
            manager.ApplyText(mnuPreviewStop, "fmain.menu.preview.stop.text");

            manager.ApplyText(mnuTools, "fmain.menu.tools.text");
            manager.ApplyText(mnuToolsExport, "fmain.menu.tools.export.text");
            manager.ApplyText(mnuToolsExportCsv, "fmain.menu.tools.export.csv.text");
            manager.ApplyText(mnuToolsImport, "fmain.menu.tools.import.text");
            manager.ApplyText(mnuToolsBuild, "fmain.menu.tools.build.text");
            manager.ApplyText(mnuToolsBuildBdb, "fmain.menu.tools.build.bdb.text");
            manager.ApplyText(mnuToolsBuildAcb, "fmain.menu.tools.build.acb.text");
            manager.ApplyText(mnuToolsSettings, "fmain.menu.tools.settings.text");

            manager.ApplyText(mnuHelp, "fmain.menu.help.text");
            manager.ApplyText(mnuHelpAbout, "fmain.menu.help.about.text");

            manager.ApplyText(gbStandard, "fmain.frame.standard.text");
            manager.ApplyText(gbEditView, "fmain.frame.edit_view.text");
            manager.ApplyText(gbNote, "fmain.frame.note.text");
            manager.ApplyText(gbMeasure, "fmain.frame.measure.text");
            manager.ApplyText(gbPostprocessing, "fmain.frame.postprocessing.text");

            manager.ApplyText(tsbProjectNew, "fmain.toolbar.standard.new.text");
            manager.ApplyText(tsbProjectOpen, "fmain.toolbar.standard.open.text");
            manager.ApplyText(tsbProjectSave, "fmain.toolbar.standard.save.text");
            manager.ApplyText(tsbEditUndo, "fmain.toolbar.standard.undo.text");
            manager.ApplyText(tsbEditRedo, "fmain.toolbar.standard.redo.text");
            manager.ApplyText(tsbEditCut, "fmain.toolbar.standard.cut.text");
            manager.ApplyText(tsbEditCopy, "fmain.toolbar.standard.copy.text");
            manager.ApplyText(tsbEditPaste, "fmain.toolbar.standard.paste.text");

            manager.ApplyText(tsbEditMode, "fmain.toolbar.edit_view.mode.text");
            manager.ApplyText(toolStripLabel1, "fmain.toolbar.edit_view.difficulty_label.text");
            manager.ApplyText(tsbViewZoomIn, "fmain.toolbar.edit_view.zoom_in.text");
            manager.ApplyText(tsbViewZoomOut, "fmain.toolbar.edit_view.zoom_out.text");

            manager.ApplyText(tsbScoreNoteResetToTap, "fmain.toolbar.note.reset_to_tap.text");
            manager.ApplyText(tsbScoreNoteDelete, "fmain.toolbar.note.delete.text");
            manager.ApplyText(toolStripLabel2, "fmain.toolbar.note.start_position_label.text");
            manager.ApplyText(tsbScoreNoteInsertSpecial, "fmain.toolbar.note.insert_special.text");

            manager.ApplyText(tsbScoreMeasureAppend, "fmain.toolbar.measure.append.text");
            manager.ApplyText(tsbScoreMeasureAppendMultiple, "fmain.toolbar.measure.append_multiple.text");
            manager.ApplyText(tsbScoreMeasureInsert, "fmain.toolbar.measure.insert.text");
            manager.ApplyText(tsbScoreMeasureInsertMultiple, "fmain.toolbar.measure.insert_multiple.text");
            manager.ApplyText(tsbScoreMeasureDelete, "fmain.toolbar.measure.delete.text");

            manager.ApplyText(tsbPreviewFromThisMeasure, "fmain.toolbar.postprocessing.preview.from_this_measure.text");
            manager.ApplyText(tsbPreviewFromStart, "fmain.toolbar.postprocessing.preview.from_start.text");
            manager.ApplyText(tsbPreviewStop, "fmain.toolbar.postprocessing.preview.stop.text");
            manager.ApplyText(tsbToolsBuildBdb, "fmain.toolbar.postprocessing.build.bdb.text");
            manager.ApplyText(tsbToolsBuildAcb, "fmain.toolbar.postprocessing.build.acb.text");

            manager.ApplyText(ctxEditCut, "fmain.context.edit.cut");
            manager.ApplyText(ctxEditCopy, "fmain.context.edit.copy");
            manager.ApplyText(ctxEditPaste, "fmain.context.edit.paste");
            manager.ApplyText(ctxScoreNoteResetToTap, "fmain.context.note.reset_to_tap");
            manager.ApplyText(ctxScoreNoteDelete, "fmain.context.note.delete");
            manager.ApplyText(ctxScoreMeasureDelete, "fmain.context.measure.delete");
            manager.ApplyText(ctxScoreNoteInsertSpecial, "fmain.context.note.insert_special");
            manager.ApplyText(ctxScoreNoteModifySpecial, "fmain.context.note.modify_special");
            manager.ApplyText(ctxScoreNoteDeleteSpecial, "fmain.context.note.delete_special");
        }

    }
}
