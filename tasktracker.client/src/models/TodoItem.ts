import States from "./States"

interface TodoItem{
    id: number;
    title: string;
    state: States
}

export default TodoItem;