import React, { useEffect, useCallback } from 'react';
import Spinner from './Spinner';

function Modal({ onClose, Header, Footer, children, loading }) {
  const handleClose = useCallback(() => {
    if (!loading) onClose();
  }, [loading, onClose]);

  useEffect(() => {
    function onKeyDown(e) {
      if (e.key === 'Escape') {
        handleClose();
      }
    }

    document.body.classList.add('modal-open');
    document.addEventListener('keydown', onKeyDown);

    return () => {
      document.body.classList.remove('modal-open');
      document.removeEventListener('keydown', onKeyDown);
    };
  }, [handleClose]);

  return (
    <div
      className="modal show"
      role="dialog"
      style={{ display: 'block', backgroundColor: 'rgba(0,0,0,0.7)' }}
    >
      <div
        role="button"
        style={{ position: 'absolute', left: 0, top: 0, right: 0, bottom: 0 }}
        onClick={handleClose}
      ></div>

      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <Header />
          </div>

          <div className="modal-body">{children}</div>

          <div className="modal-footer">
            <Footer />
          </div>

          {loading && <Spinner />}
        </div>
      </div>
    </div>
  );
}

export default Modal;
