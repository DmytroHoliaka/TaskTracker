interface TodoItem{
    id: number;
    title: string;
    status: 'todo' | 'in progres' | 'done';
}

export default TodoItem;