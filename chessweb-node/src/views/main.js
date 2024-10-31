document.addEventListener('DOMContentLoaded', () => {
    const chessBoard = document.getElementById('chess-board');
  
    // Function to fetch the chess position from the server
    async function fetchChessPosition(fen) {
      try {
        const response = await fetch(`/position?fen=${encodeURIComponent(fen)}`);
        const position = await response.json();
        renderChessBoard(position.fen);
      } catch (error) {
        console.error('Error fetching chess position:', error);
      }
    }
  
    // Function to render the chess board
    function renderChessBoard(fen) {
      // Clear the existing board
      chessBoard.innerHTML = '';
  
      // Convert FEN to a 2D array representation of the board
      const rows = fen.split(' ')[0].split('/');
      rows.forEach((row, rowIndex) => {
        const rowDiv = document.createElement('div');
        rowDiv.classList.add('chess-row');
        let colIndex = 0;
  
        for (const char of row) {
          if (isNaN(char)) {
            const cellDiv = document.createElement('div');
            cellDiv.classList.add('chess-cell');
            cellDiv.textContent = char;
            rowDiv.appendChild(cellDiv);
            colIndex++;
          } else {
            colIndex += parseInt(char, 10);
          }
        }
  
        chessBoard.appendChild(rowDiv);
      });
    }
  
    // Example FEN string for initial load
    const initialFEN = 'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR';
    fetchChessPosition(initialFEN);
  });