import React, { useState } from "react";
import TodoItem from "../models/TodoItem";
import { MdDelete, MdEdit } from "react-icons/md";
import { IoCheckmarkDoneSharp } from "react-icons/io5";
import "./styles.css";
import { Draggable } from "react-beautiful-dnd";
import { UpdateTask } from "../services/requests";

interface Props {
  arrayIndex: number;
  task: TodoItem;
  tasks: TodoItem[];
  setTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
  openModal: (
    task: TodoItem,
    taskArray: TodoItem[],
    setTaskArray: React.Dispatch<React.SetStateAction<TodoItem[]>>
  ) => void;
}

const SingleTodo: React.FC<Props> = ({
  arrayIndex,
  task,
  tasks,
  setTasks,
  openModal,
}) => {
  const [isEditMode, setIsEditMode] = useState<boolean>(false);
  const [newTitle, setNewTitle] = useState<string>(task.title);

  const handleEdit = async (e: React.FormEvent): Promise<void> => {
    e.preventDefault();

    const newTask: TodoItem = {
      id: task.id,
      title: newTitle,
      state: task.state,
    };

    if (
      task.title !== newTask.title &&
      (await UpdateTask(newTask.id, newTask))
    ) {
      setTasks(tasks.map((t) => (t.id === task.id ? newTask : t)));
    }

    setIsEditMode(false);
  };

  return (
    <Draggable draggableId={task.id.toString()} index={arrayIndex}>
      {(provider, snapshot) => (
        <form
          onSubmit={(e) => handleEdit(e)}
          {...provider.draggableProps}
          {...provider.dragHandleProps}
          ref={provider.innerRef}
          className={`task-item ${snapshot.isDragging ? "drag" : ""}`}
        >
          {isEditMode ? (
            <input
              type="text"
              value={newTitle}
              placeholder="Edit new task..."
              onChange={(e) => setNewTitle(e.target.value)}
              maxLength={32}
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
              {isEditMode === true ? <IoCheckmarkDoneSharp /> : <MdEdit />}
            </span>
            <span
              onClick={() => openModal(task, tasks, setTasks)}
              className="icon"
            >
              {<MdDelete />}
            </span>
          </div>
        </form>
      )}
    </Draggable>
  );
};

export default SingleTodo;
