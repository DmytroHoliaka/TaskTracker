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
            <SingleTodo task={task} tasks={todoTasks} setTasks={setTodoTasks} />
          ))}
        </div>
      </div>

      <div className="status-block">
        <span className="list-title">In progress</span>
        <div className="task-block">
          {inProgressTasks.map((task) => (
            <SingleTodo
              task={task}
              tasks={inProgressTasks}
              setTasks={setInProgressTasks}
            />
          ))}
        </div>
      </div>

      <div className="status-block">
        <span className="list-title">Done</span>
        <div className="task-block">
          {doneTasks.map((task) => (
            <SingleTodo task={task} tasks={doneTasks} setTasks={setDoneTasks} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default TodoList;
