import React, { useState } from "react";
import TodoItem from "../models/TodoItem";
import { MdDelete, MdEdit } from "react-icons/md";
import "./styles.css";

interface Props {
  task: TodoItem;
  tasks: TodoItem[];
  setTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
}

const SingleTodo: React.FC<Props> = ({ task, tasks, setTasks }) => {
  const [isEditMode, setIsEditMode] = useState<boolean>(false);
  const [newTitle, setNewTitle] = useState<string>(task.title);

  const handleEdit = (e: React.FormEvent): void => {
    e?.preventDefault();

    setTasks(
      tasks.map((t) => (t.id === task.id ? { ...t, title: newTitle } : t))
    );

    setIsEditMode(false);
  };

  const handleDelete = (): void => {
    setTasks(tasks.filter((t) => t.id !== task.id));
  };

  return (
    <form className="task-item" onSubmit={(e) => handleEdit(e)}>
      {isEditMode ? (
        <input
          type="text"
          value={newTitle}
          placeholder="Edit new task..."
          onChange={(e) => setNewTitle(e.target.value)}
          className="edit-input"
        ></input>
      ) : (
        <div className="task-title">{task.title}</div>
      )}
      <div className="icons">
        <span
          onClick={(e) => {
            if (isEditMode === false) {
              setIsEditMode(true);
            } else {
              handleEdit(e);
            }
          }}
          className="icon"
        >
          {<MdEdit />}
        </span>
        <span onClick={() => handleDelete()} className="icon">
          {<MdDelete />}
        </span>
      </div>
    </form>
  );
};

export default SingleTodo;
