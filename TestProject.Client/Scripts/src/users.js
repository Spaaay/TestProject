import React from 'react';
import axios from 'axios';
import ModalWindow from './modal';
import '../../Content/Table.css';
import styles from '../../Content/Site.css';

export default class User extends React.Component {
	constructor () {
		super();
		this.openModal = this.openModal.bind(this);
		this.afterOpenModal = this.afterOpenModal.bind(this);
		this.closeModal = this.closeModal.bind(this);
        this.setFullName = this.setFullName.bind(this);
        this.setLogin = this.setLogin.bind(this);
        this.setPassword = this.setPassword.bind(this);
        this.setGroupId = this.setGroupId.bind(this);
	}

	state = {
		users: [],
		modalIsOpen: false,
		userModel: {
			Id: undefined,
			FullName: undefined,
            Login: undefined,
            Password: undefined,
            GroupId: undefined
		},
		isNewUser: false
	};

	blockStyle = {
    	color: 'white',
    	fontSize: 200,
    	height: '100%'
	};

	openModal (item) {
		this.setState({ modalIsOpen: true, userModel: { Id: item.Id, FullName: item.FullName, Login: item.Login, Password: item.Password, GroupId: item.GroupId } });
	}

	afterOpenModal () {
	}

	closeModal (isNeedSave) {
		let root = this;
		this.setState({ modalIsOpen: false });
		if (isNeedSave && this.state.userModel.Id !== undefined) {
			if (this.state.isNewUser) {
				axios.post('http://localhost:8080/api/user/', root.state.userModel)
					.then(function (response) {
						root.setState({ isNewUser: false }, () => { root.getAllUsers(); });
					}).catch(function (response) {
					});
			} else {
				axios.put('http://localhost:8080/api/user/', root.state.userModel)
					.then(function (response) {
						root.getAllUsers();
					}).catch(function (response) {
					});
			}
		}
	}

	componentDidMount (nextProps) {
		this.getAllUsers(this);
	}

	deleteUser (id) {
		let root = this;
    	axios.delete('http://localhost:8080/api/user/' + id)
    		.then(function (response) {
				root.getAllUsers();
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	getAllUsers () {
    	let root = this;
    	axios.get('http://localhost:8080/api/user')
    		.then(function (response) {
    			let res = JSON.parse(response.data);
    			root.setState({ users: res });
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	setFullName (val) {
		let user = { ...this.state.userModel };
		user.FullName = val.target.value;
		this.setState({ userModel: user });
	}

	setLogin (val) {
		let user = { ...this.state.userModel };
		user.Login = val.target.value;
		this.setState({ userModel: user });
    }
    
    setPassword (val) {
		let user = { ...this.state.userModel };
		user.Password = val.target.value;
		this.setState({ userModel: user });
    }
    setGroupId (val) {
		let user = { ...this.state.userModel };
		user.GroupId = val.target.value;
		this.setState({ userModel: user });
	}

	renderTableRows (array) {
    	return array.map(item =>
    		<tr key={item.Id}>
    			<td>{item.FullName}</td>
    			<td>{item.Login}</td>
    			<td>{item.Password}</td>
                <td>{item.GroupId}</td>
    			<td><input type='image' name="redact" onClick={() => this.openModal(item)} src="./Content/Images/pencil_and_paper-512.png" width="20"/></td>
    			<td><input type='image' onClick={() => this.deleteUser(item.Id)} name="delete" src="./Content/Images/filled-trash.png" width="25"/></td>
    		</tr>
    	);
	}

	render () {
		let root = this;
    	return <div>
    		<div style={this.blockStyle}>
    			<table>
    				<thead>
    					<tr>
    						<th>ФИО</th>
    						<th>Логин</th>
    						<th>Пароль</th>
                            <th>Группа</th>
    					</tr>
    				</thead>
    				<tbody>
    					{this.renderTableRows(this.state.users)}
    				</tbody>
    			</table>
    			<hr/>
    		</div>
			<div style={{ textAlign: 'center' }}>
				<input
					className={styles.addUserInput}
					onClick={() => this.setState({ isNewUser: true }, () => { root.openModal({ Id: 0, FullName: '', Login: '', Password: '', GroupId: '' }); }) }
					type='image'
					name="addGroup"
					src="./Content/Images/add.png"
					width="25">
				</input>
			</div>
			<ModalWindow
				isOpen={this.state.modalIsOpen}
				onAfterOpen={this.afterOpenModal}
				onRequestClose={this.closeModal}
				item={this.state.userModel}
				onClose={this.closeModal}
				modalHeader='Введите данные'
				inputCollection = {[
					{ value: this.state.userModel.FullName, header: 'ФИО', onChangeFn: this.setFullName },
					{ value: this.state.userModel.Login, header: 'Login', onChangeFn: this.setLogin },
					{ value: this.state.userModel.Password, header: 'Password', onChangeFn: this.setPassword },
                    { value: this.state.userModel.GroupId, header: 'Группа', onChangeFn: this.setGroupId }
				]}
			/>
    	</div>;
	}
}
