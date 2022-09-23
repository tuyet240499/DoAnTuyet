import React, {Component} from 'react'
import { ScrollView } from 'react-native';
import { StyleSheet, Text, View, TextInput, 
    KeyboardAvoidingView,TouchableOpacity, Button, Dimensions, Image} from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import FontAwesome from 'react-native-vector-icons/FontAwesome'
import MqttService  from "../src/core/services/MqttService";
import OfflineNotification from "../src/core/components/OfflineNotification";
var width= 340;

export default class CartScreen extends React.Component {
    constructor(props){
        super(props);
        this.state={
            dataCart:[],
            message:"",
            isConnected:false,
        };
    }

    componentDidMount(){
        MqttService.connectClient(
            this.mqttSuccessHandler,
            this.mqttConnectionLostHandler
          );
        const data = [
            {name:'coca', quantity: 12, gia: '24000 NVĐ'},
            {name:'pesi', quantity: 10, gia: '12000 NVĐ'},
            {name:'mì tôm', quantity: 11, gia: '30000 NVĐ'},
            {name:'sữa chua', quantity: 12, gia: '50000 NVĐ'},
            {name:'giấy A4', quantity: 10, gia: '24000 NVĐ'},
          ]; 
          this.setState({dataCart:data})
    }
    onWORLD = message => {
        console.info("message");
        var arr =String(message).replace('"','').split("-");
            var cus =arr[0];
            var cartid =arr[1].replace('"','');
        this.setState({
          message:cus
        });
         alert(cus);
        
      };
      mqttSuccessHandler = () => {
        this.setState({
          isConnected: true
        });
        console.info("connected to mqtt");
        MqttService.subscribe("takePro", this.onWORLD);
        
      };
      mqttConnectionLostHandler = () => {
        console.info("Fail connect to mqtt");
        /*this.setState({
          isConnected: false
        });*/
      };
    
    render() {
        
        return(
            <View style={{flex:1, alignItems: 'center', justifyContent: 'center',backgroundColor:'#fff'}}>
                <View style={{height:20}}/>
                <Text>{this.state.message}</Text>
                <View style={{ borderBottomWidth:2, borderColor:'#cccccc', width:width+10, flexDirection:'row',justifyContent:'center'}}>
                <FontAwesome 
                    name="money"
                    color={'#009387'}
                    size={25}
                />
                        <Text style={{color:'#009387', fontSize:18, fontWeight:'bold',marginLeft:20, marginBottom:30}}>Tổng tiền: {this.state.message}</Text>
                </View>  
                <View style={{height:10}}/>
                <ScrollView>

                    {
                        this.state.dataCart.map((item)=>{
                            return(
                                <View>
                                
                            <View style={{backgroundColor:'#ffff', flex:1, justifyContent:'space-between', borderBottomWidth:2, borderColor:'#cccccc'}}> 
                    <View style={{width:width+10, flexDirection:'row'}}>
                        <Image style={{width:width/4, height:width/4}} source={require('./coca.jpg')}></Image>
                        <View style={{flex:1, marginLeft:40}}>
                            <Text style={styles.listSP}>{item.name} </Text>
                            <Text style={styles.listSP}>Số lượng: {item.quantity}</Text>
                            
                            <Text style={{fontSize:16, color: "#33c37d",fontWeight:'bold', marginBottom: 20}}>Giá: {item.gia}</Text>
                            
                        </View>
                    </View>               
                </View>
                <View style={{height:20}}/></View>
                            )
                        })
                    }
                </ScrollView>
                             
                <View style={styles.button}>
                <TouchableOpacity style={styles.signIn}>
                <LinearGradient
                    colors={['#08d4c4', '#01ab9d']}
                    style={styles.signIn}>
                    <Text style={[styles.textSign, {
                        color:'#fff'
                    }]}>THANH TOÁN</Text>
                </LinearGradient>
                </TouchableOpacity>
            </View>
            <View style={{height:20}}/>
            </View>
        );
    }
}
 const styles = StyleSheet.create({
    container: {
      flex: 1, 
      backgroundColor: '#009387'
    },
    textInput: {
        flex: 1,
        marginTop: Platform.OS === 'ios' ? 0 : -12,
        paddingLeft: 10,
        color: '#05375a',
    },
    button: {
        alignItems: 'center',
        marginTop: 50, 
        width: 300,
    },
    signIn: {
        width: '100%',
        height: 50,
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 10
    },
    textSign: {
        fontSize: 18,
        fontWeight: 'bold'
    },
    listSP:{
        fontSize:16
    }
  });

