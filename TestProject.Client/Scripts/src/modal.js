import React from 'react';
import Modal from 'react-modal';

Modal.setAppElement('#app');

export default class ModalWindow extends React.Component {
    state = {
    	modalIsOpen: true
    }

    render () {
    	return <Modal
    		isOpen={this.props.isOpen}
    		onAfterOpen={this.props.onAfterOpen}
    		onRequestClose={this.props.onRequestClose}
    		style={customStyles}
    	>

    		<h3>{this.props.modalHeader}</h3>
    		<div style={{ display: 'flex', flexDirection: 'column' }}>
    			{this.props.inputCollection.map((item, i) => {
    				return <div>
    					<p>{item.header}</p>
    					<input value={item.value} onChange={(val) => item.onChangeFn(val)} style={modalInput}/>
    				</div>;
    			})}
    		</div>
    		<div style={modalButtonContainer}>
    			<button style={modalButtonLeft} onClick={() => this.props.onClose(true)}>{this.props.isSignBtn ? 'Войти' : 'Сохранить'}</button>
    			{!this.props.isSignBtn && <button style={modalButtonRight} onClick={() => this.props.onClose(false)}>Закрыть</button>}
    		</div>
    	</Modal>;
    }
}

const customStyles = {
	content: {
		top: '50%',
		left: '50%',
		right: 'auto',
		bottom: 'auto',
		marginRight: '-50%',
		transform: 'translate(-50%, -50%)'
	}
};

const modalButtonContainer = {
	display: 'flex',
	flexDirection: 'row',
	flex: '1 1 auto',
	marginTop: '25px'
};

const modalButtonLeft = {
	flexGrow: 'inherit',
	marginRight: '20px'
};

const modalButtonRight = {
	flexGrow: 'inherit',
	marginLeft: '20px'
};

const modalInput = {
	border: 'none',
	borderBottom: '1px solid',
	width: '100%'
};
