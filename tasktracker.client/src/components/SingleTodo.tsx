import React from "react";
import TodoItem from "../models/TodoItem";
import { MdDelete, MdEdit } from "react-icons/md";

interface Props {
  task: TodoItem;
}

const SingleTodo: React.FC<Props> = ({ task }) => {
  return (
    <div className="task-item">
      <div className="task-title">{task.title}</div>
      <div className="icons">
        <div className="icon">{<MdEdit />}</div>
        <div className="icon">{<MdDelete />}</div>
      </div>
    </div>
  );
};

export default SingleTodo;
