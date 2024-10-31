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
        const chessBoard = document.getElementById('chess-board');
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
                    cellDiv.classList.add((rowIndex + colIndex) % 2 === 0 ? 'light-square' : 'dark-square');
                    cellDiv.dataset.position = `${rowIndex}-${colIndex}`;

                    const img = document.createElement('img');
                    img.src = `images/wikipedia/${char === char.toLowerCase() ? 'b' : 'w'}${char.toUpperCase()}.png`;
                    img.alt = char;
                    img.classList.add('chess-piece');
                    img.draggable = true;
                    img.dataset.position = `${rowIndex}-${colIndex}`;
                    img.addEventListener('dragstart', handleDragStart);
                    cellDiv.appendChild(img);

                    cellDiv.addEventListener('dragover', handleDragOver);
                    cellDiv.addEventListener('drop', handleDrop);

                    rowDiv.appendChild(cellDiv);
                    colIndex++;
                } else {
                    const emptySquares = parseInt(char, 10);
                    for (let i = 0; i < emptySquares; i++) {
                        const cellDiv = document.createElement('div');
                        cellDiv.classList.add('chess-cell');
                        cellDiv.classList.add((rowIndex + colIndex) % 2 === 0 ? 'light-square' : 'dark-square');
                        cellDiv.dataset.position = `${rowIndex}-${colIndex}`;

                        cellDiv.addEventListener('dragover', handleDragOver);
                        cellDiv.addEventListener('drop', handleDrop);

                        rowDiv.appendChild(cellDiv);
                        colIndex++;
                    }
                }
            }

            chessBoard.appendChild(rowDiv);
        });
    }

    // Drag and Drop Handlers
    function handleDragStart(event) {
        event.dataTransfer.setData('text/plain', event.target.dataset.position);
    }

    function handleDragOver(event) {
        event.preventDefault();
    }

    function handleDrop(event) {
        event.preventDefault();
        const sourcePosition = event.dataTransfer.getData('text/plain');
        let targetPosition = event.target.dataset.position;

        // If the drop event is fired on the image itself, get the parent cell's position
        if (!targetPosition && event.target.parentElement) {
            targetPosition = event.target.parentElement.dataset.position;
        }

        if (sourcePosition !== targetPosition) {
            const sourceCell = document.querySelector(`[data-position="${sourcePosition}"]`);
            const targetCell = document.querySelector(`[data-position="${targetPosition}"]`);

            const piece = sourceCell.querySelector('img');
            if (piece) {
                sourceCell.removeChild(piece);
                targetCell.innerHTML = '';
                targetCell.appendChild(piece);

                // Update the data-position attribute of the moved piece
                piece.dataset.position = targetPosition;

                // Reattach drag event listeners to the moved piece
                piece.addEventListener('dragstart', handleDragStart);

                // Ensure the target cell can handle drop events
                targetCell.addEventListener('dragover', handleDragOver);
                targetCell.addEventListener('drop', handleDrop);

                // Ensure the source cell can handle drop events
                sourceCell.addEventListener('dragover', handleDragOver);
                sourceCell.addEventListener('drop', handleDrop);
            
                // Ensure the piece can be dragged again
                piece.draggable = true;
            }
        }
    }

    // Example FEN string for initial load
    const initialFEN = 'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR';
    fetchChessPosition(initialFEN);
  });