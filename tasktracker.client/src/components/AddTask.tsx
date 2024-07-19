import React, { useState } from 'react';
import axios from 'axios';

interface AddTaskProps {
    onTaskAdded: () => void;
}

const AddTask: React.FC<AddTaskProps> = ({ onTaskAdded }) => {
    const [title, setTitle] = useState('');

    const handleAddTask = async () => {
        if (title) {
            try {
                await axios.post('https://localhost:7068/api/ToDoItems', { title, status: 'ToDo' });
                setTitle('');
                onTaskAdded();
            } catch (error) {
                console.error('Error adding task:', error);
            }
        }
    };

    return (
        <div className="add-task">
            <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Enter a task"
            />
            <button onClick={handleAddTask}>Add</button>
        </div>
    );
};

export default AddTask;
