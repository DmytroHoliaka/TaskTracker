import React, { useState } from "react";
import "./App.css";
import InputForm from "./components/InputForm";
import TodoItem from "./models/TodoItem";
import TodoList from "./components/TodoList";

const App: React.FC = () => {
  const [newTaskTitle, setNewTaskTitle] = useState<string>("");
  const [todoTasks, setTodoTasks] = useState<TodoItem[]>([]);
  const [inProgressTasks, setInProgressTasks] = useState<TodoItem[]>([]);
  const [doneTasks, setDoneTasks] = useState<TodoItem[]>([]);

  const handleCreate = (e: React.FormEvent<HTMLFormElement>): void => {
    e.preventDefault();

    if (newTaskTitle) {
      setTodoTasks([
        ...todoTasks,
        { id: Date.now(), title: newTaskTitle, status: "todo" },
      ]);
      setNewTaskTitle("");
    }
  };

  return (
    <div className="app">
      <div className="upper-container">
        <div className="header-block">
          <span className="header">Task Tracker</span>
        </div>
        <div className="input-block">
          <InputForm
            taskTitle={newTaskTitle}
            setTaskTitle={setNewTaskTitle}
            handleCreate={handleCreate}
          />
        </div>
      </div>
      <div className="lower-container">
        <TodoList
          todoTasks={todoTasks}
          setTodoTasks={setTodoTasks}
          inProgressTasks={inProgressTasks}
          setInProgressTasks={setInProgressTasks}
          doneTasks={doneTasks}
          setDoneTasks={setDoneTasks}
        />
      </div>
    </div>
  );
};

export default App;
