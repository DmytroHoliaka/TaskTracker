import React, { useState } from "react";
import TodoItem from "../models/TodoItem";
import SingleTodo from "./SingleTodo";

interface Props {
  todoTasks: TodoItem[];
  setTodoTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
  inProgressTasks: TodoItem[];
  setInProgressTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
  doneTasks: TodoItem[];
  setDoneTasks: React.Dispatch<React.SetStateAction<TodoItem[]>>;
}

const TodoList: React.FC<Props> = ({
  todoTasks,
  setTodoTasks,
  inProgressTasks,
  setInProgressTasks,
  doneTasks,
  setDoneTasks,
}) => {
  return (
    <div className="list-container">
      <div className="status-block">
        <span className="list-title">To do</span>
        <div className="task-block">
          {todoTasks.map((task) => (
            <SingleTodo task={task} />
          ))}
        </div>
      </div>

      <div className="status-block">
        <span className="list-title">In progress</span>
        <div className="task-block">
          {todoTasks.map((task) => (
            <SingleTodo task={task} />
          ))}
        </div>
      </div>

      <div className="status-block">
        <span className="list-title">Done</span>
        <div className="task-block">
          {todoTasks.map((task) => (
            <SingleTodo task={task} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default TodoList;