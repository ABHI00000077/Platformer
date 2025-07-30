mergeInto(LibraryManager.library, {
  onGameComplete: function(level) {
    // Call the global JS function (which you define in index.html)
    if (typeof window.onGameComplete === "function") {
      window.onGameComplete(level);
    }
  }
});