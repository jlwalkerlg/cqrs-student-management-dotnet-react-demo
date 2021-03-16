import React from 'react';

function Spinner() {
  return (
    <div
      className="d-flex justify-content-center align-items-center"
      style={{
        position: 'absolute',
        left: 0,
        top: 0,
        right: 0,
        bottom: 0,
        backgroundColor: 'rgba(255,255,255,0.5)',
      }}
    >
      <div className="spinner-border text-secondary" role="status">
        <span className="sr-only">Loading...</span>
      </div>
    </div>
  );
}

export default Spinner;
