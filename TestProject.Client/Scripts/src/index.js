import React from 'react';
import { render } from 'react-dom';
import Teacher from './teachers';

class App extends React.Component {
	render () {
		return <div><Teacher/></div>;
	}
}

render(<App/>, document.getElementById('app'));
