import States from "../models/States";
import TodoItem from "../models/TodoItem";

const parseTodoItem = (data: any): TodoItem => {
  let parsedState: States;

  if (data?.state === 0) {
    parsedState = States.Todo;
  } else if (data?.state === 1) {
    parsedState = States.InProgress;
  } else if (data?.state === 2) {
    parsedState = States.Done;
  } else {
    parsedState = States.ErrorState;
    console.log(`Get incorrect state while parse input data (state: ${data.state})`)
  }

  return {
    id: data?.id,
    title: data?.title,
    state: parsedState,
  };
};

const parseTodoItems = (data: any[]): TodoItem[] => {
  return data.map(parseTodoItem);
};

export default parseTodoItems;
