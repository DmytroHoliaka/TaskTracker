import React, { useEffect, useState } from "react";
import "./App.css";
import InputForm from "./components/InputForm";
import TodoItem from "./models/TodoItem";
import TodoList from "./components/TodoList";
import { DragDropContext, DropResult } from "react-beautiful-dnd";
import { fetchTasks } from "./services/requests";
import States from "./models/States";

const App: React.FC = () => {
  useEffect(() => {
    const fetchData = async () => {
      let allTasks: TodoItem[] = await fetchTasks();

      const inProgress: TodoItem[] = [];
      const done: TodoItem[] = [];
      const todo: TodoItem[] = [];

      allTasks.forEach((task) => {
        if (task.state === States.Todo) {
          todo.push(task);
        } else if (task.state === States.InProgress) {
          inProgress.push(task);
        } else if (task.state === States.Done) {
          done.push(task);
        } else {
          console.log(
            `Getting an incorrect state when retrieving tasks from the database (state: ${task.state})`
          );
        }
      });

      setInProgressTasks(inProgress);
      setDoneTasks(done);
      setTodoTasks(todo);
    };

    fetchData();
  }, []);

  const [newTaskTitle, setNewTaskTitle] = useState<string>("");
  const [todoTasks, setTodoTasks] = useState<TodoItem[]>([]);
  const [inProgressTasks, setInProgressTasks] = useState<TodoItem[]>([]);
  const [doneTasks, setDoneTasks] = useState<TodoItem[]>([]);

  const handleCreate = (e: React.FormEvent<HTMLFormElement>): void => {
    e.preventDefault();

    if (newTaskTitle) {
      setTodoTasks([
        ...todoTasks,
        { id: Date.now(), title: newTaskTitle, state: States.Todo },
      ]);
      setNewTaskTitle("");
    }
  };

  const onDragEnd = (result: DropResult): void => {
    const { destination, source } = result;

    if (!destination) {
      return;
    }

    if (
      source.droppableId === destination.droppableId &&
      source.index === destination.index
    ) {
      return;
    }

    let add;
    let todo = todoTasks;
    let inProgress = inProgressTasks;
    let done = doneTasks;

    if (source.droppableId === "TodoBlock") {
      add = todo[source.index];
      todo.splice(source.index, 1);
    } else if (source.droppableId === "InProgressBlock") {
      add = inProgress[source.index];
      inProgress.splice(source.index, 1);
    } else {
      add = done[source.index];
      done.splice(source.index, 1);
    }

    if (destination.droppableId === "TodoBlock") {
      todo.splice(destination.index, 0, add);
    } else if (destination.droppableId === "InProgressBlock") {
      inProgress.splice(destination.index, 0, add);
    } else {
      done.splice(destination.index, 0, add);
    }

    setTodoTasks(todo);
    setInProgressTasks(inProgress);
    setDoneTasks(done);
  };

  return (
    <DragDropContext onDragEnd={onDragEnd}>
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
    </DragDropContext>
  );
};

export default App;
