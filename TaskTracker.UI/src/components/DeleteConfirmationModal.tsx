import React from "react";
import TodoItem from "../models/TodoItem";
import { MdDeleteSweep } from "react-icons/md";

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: () => void;
  task: TodoItem | null;
}

const DeleteConfirmationModal: React.FC<ModalProps> = ({
  isOpen,
  onClose,
  onConfirm,
  task,
}) => {
  if (!isOpen) return null;

  return (
    <div className="modal-content">
      <span className="modal-content-icon">{<MdDeleteSweep />}</span>
      <span className="modal-content-title">Confirm Delete</span>
      <p className="modal-content-description">
        Are you sure you want to delete the task "{task?.title}"?
      </p>
      <div className="modal-content-delete-buttons">
        <button onClick={() => onClose()} className="modal-content-delete-button">
          Cancel
        </button>
        <button onClick={() => onConfirm()} className="modal-content-delete-button">
          Submit
        </button>
      </div>
    </div>
  );
};

export default DeleteConfirmationModal;
