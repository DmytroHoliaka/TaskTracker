import React, { useRef } from "react";
import "./styles.css";
import TodoItem from "../models/TodoItem";

interface Props {
  taskTitle: string;
  setTaskTitle: React.Dispatch<React.SetStateAction<string>>;
  handleCreate: (e: React.FormEvent<HTMLFormElement>) => void;
}

const InputForm: React.FC<Props> = ({
  taskTitle,
  setTaskTitle,
  handleCreate,
}) => {
  const inputRef = useRef<HTMLInputElement>(null);

  return (
    <form
      className="input"
      onSubmit={(e) => {
        handleCreate(e);
        inputRef.current?.blur();
      }}
    >
      <input
        type="text"
        placeholder="Enter a task..."
        value={taskTitle}
        onChange={(e) => setTaskTitle(e.target.value)}
        ref={inputRef}
        className="input__box"
      ></input>
      <button type="submit" className="input__submit">
        Add
      </button>
    </form>
  );
};

export default InputForm;
