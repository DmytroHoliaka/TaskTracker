import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { DragDropContext, DropResult } from 'react-beautiful-dnd';
import AddTask from './components/AddTask';
import Column from './components/Column';
import { ToDoItem } from './types';
import './App.css';

const App: React.FC = () => {
    const [tasks, setTasks] = useState<ToDoItem[]>([]);

    const fetchTasks = async () => {
        try {
            const response = await axios.get('/api/ToDoItems');
            if (Array.isArray(response.data)) {
                setTasks(response.data);
            } else {
                console.error('Unexpected response data:', response.data);
                setTasks([]);
            }
        } catch (error) {
            console.error('Error fetching tasks:', error);
            setTasks([]);
        }
    };

    useEffect(() => {
        fetchTasks();
    }, []);

    const handleTaskAdded = () => {
        fetchTasks();
    };

    const handleTaskDeleted = (id: number) => {
        setTasks(tasks.filter(task => task.id !== id));
    };

    const handleDragEnd = async (result: DropResult) => {
        const { destination, source, draggableId } = result;

        if (!destination) return;

        if (destination.droppableId === source.droppableId && destination.index === source.index) {
            return;
        }

        const updatedTasks = Array.from(tasks);
        const movedTask = updatedTasks.find(task => task.id === parseInt(draggableId))!;
        movedTask.status = destination.droppableId;

        setTasks(updatedTasks);
        await axios.put(`/api/ToDoItems/${movedTask.id}`, movedTask);
    };

    return (
        <div className="App">
            <div className="header">
                <AddTask onTaskAdded={handleTaskAdded} />
            </div>
            <DragDropContext onDragEnd={handleDragEnd}>
                <div className="columns">
                    {['ToDo', 'In progress', 'Done'].map(status => (
                        <Column
                            key={status}
                            title={status}
                            tasks={tasks.filter(task => task.status === status)}
                            onDelete={handleTaskDeleted}
                        />
                    ))}
                </div>
            </DragDropContext>
        </div>
    );
};

export default App;
