import React from 'react';
import axios from 'axios';

export default class Teacher extends React.Component {
    state = {};

    blockStyle = {
    	color: 'white',
    	fontSize: 200,
    	width: '500px',
    	height: '100%'
    };

    getAllTeachers () {
    	let res = axios.get('http://localhost:8090/test69/api/Teacher/GetAllTeachers').then(function(response){
			debugger;
		}).catch(function(response){
			debugger;
		});
    }

    render () {
    	return <div style={this.blockStyle}>
    		<button onClick={this.getAllTeachers}>Преподаватели</button>
    	</div>;
    }
}
