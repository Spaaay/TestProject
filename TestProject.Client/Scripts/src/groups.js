import React from 'react';
import axios from 'axios';
import ModalWindow from './modal';
import '../../Content/Table.css';
import styles from '../../Content/Site.css';

export default class Group extends React.Component {
	constructor () {
		super();
		this.openModal = this.openModal.bind(this);
		this.afterOpenModal = this.afterOpenModal.bind(this);
		this.closeModal = this.closeModal.bind(this);
		this.setGroupName = this.setGroupName.bind(this);
        this.setGroupSetStartDate = this.setGroupSetStartDate.bind(this);
		this.setGroupSetEndDate = this.setGroupSetEndDate.bind(this);
	}

	state = {
		groups: [],
		modalIsOpen: false,
		groupModel: {
			Id: undefined,
			GroupName: undefined,
            StartDate: undefined,
            EndDate: undefined,
            Users: undefined
		},
		isNewGroup: false
	};

	blockStyle = {
    	color: 'white',
    	fontSize: 200,
    	height: '100%'
	};

	openModal (item) {
		this.setState({ modalIsOpen: true, groupModel: { Id: item.Id, GroupName: item.GroupName, StartDate: item.StartDate, EndDate: item.EndDate } });
	}

	afterOpenModal () {
	}

	closeModal (isNeedSave) {
		let root = this;
		this.setState({ modalIsOpen: false });
		if (isNeedSave && this.state.groupModel.Id !== undefined) {
			if (this.state.isNewGroup) {
				axios.post('http://localhost:8080/api/group/', root.state.groupModel)
					.then(function (response) {
						root.setState({ isNewGroup: false }, () => { root.getAllGroups(); });
					}).catch(function (response) {
					});
			} else {
				axios.put('http://localhost:8080/api/group/', root.state.groupModel)
					.then(function (response) {
						root.getAllGroups();
					}).catch(function (response) {
					});
			}
		}
	}

	componentDidMount (nextProps) {
		this.getAllGroups(this);
	}

	deleteGroup (id) {
		let root = this;
    	axios.delete('http://localhost:8080/api/group/' + id)
    		.then(function (response) {
				root.getAllGroups();
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	getAllGroups () {
    	let root = this;
    	axios.get('http://localhost:8080/api/group')
    		.then(function (response) {
    			let res = JSON.parse(response.data);
    			root.setState({ groups: res });
    	}).catch(function (response) {
    		console.log(response);
    	});
	}

	setGroupName (val) {
		let group = { ...this.state.groupModel };
		group.GroupName = val.target.value;
		this.setState({ groupModel: group });
	}

	setGroupSetStartDate (val) {
		let group = { ...this.state.groupModel };
		group.StartDate = val.target.value;
		this.setState({ groupModel: group });
    }
    
    setGroupSetEndDate (val) {
		let group = { ...this.state.groupModel };
		group.EndDate = val.target.value;
		this.setState({ groupModel: group });
	}

	renderTableRows (array) {
    	return array.map(item =>
    		<tr key={item.Id}>
    			<td>{item.GroupName}</td>
    			<td>{item.StartDate}</td>
    			<td>{item.EndDate}</td>
                <td>{item.Users.map((item, i) => { return item.FullName; }).join(', ')}</td>
    			<td><input type='image' name="redact" onClick={() => this.openModal(item)} src="./Content/Images/pencil_and_paper-512.png" width="20"/></td>
    			<td><input type='image' onClick={() => this.deleteGroup(item.Id)} name="delete" src="./Content/Images/filled-trash.png" width="25"/></td>
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
    						<th>Имя</th>
    						<th>Начало</th>
    						<th>Конец</th>
                            <th>Студенты</th>
    					</tr>
    				</thead>
    				<tbody>
    					{this.renderTableRows(this.state.groups)}
    				</tbody>
    			</table>
    			<hr/>
    		</div>
			<div style={{ textAlign: 'center' }}>
				<input
					className={styles.addUserInput}
					onClick={() => this.setState({ isNewGroup: true }, () => { root.openModal({ Id: 0, GroupName: '', StarDate: '', EndDate: '' }); }) }
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
				item={this.state.groupModel}
				onClose={this.closeModal}
				modalHeader='Введите данные'
				inputCollection = {[
					{ value: this.state.groupModel.GroupName, header: 'Имя', onChangeFn: this.setGroupName },
					{ value: this.state.groupModel.StartDate, header: 'Начало', onChangeFn: this.setGroupSetStartDate },
					{ value: this.state.groupModel.EndDate, header: 'Конец', onChangeFn: this.setGroupSetEndDate }
				]}
			/>
    	</div>;
	}
}
