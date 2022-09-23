import React from 'react';
import { View, Text, Button, StyleSheet, StatusBar, Image, TextInput } from 'react-native';
import * as Animatable from 'react-native-animatable';
import { useTheme } from '@react-navigation/native';
var width= 500;
const InforCustomScreen = () => {

  const { colors } = useTheme();

  const theme = useTheme();
  
    return (
      <View style={{backgroundColor:'#ffff'}}>
        <View style={{height:20}}/>
        <View style={{alignItems:'center'}}>
          <Image style={{width:width/4, height:width/4, marginBottom:30}} source={require('./Avt.jpg')}></Image> 
        </View>
        <View style={{marginLeft:30}}>
        <View style={{ flexDirection:'row'}}> 
          <View>
            <Text style={{fontSize:20, marginRight: 15, marginTop:10, fontWeight:'bold'}}>Mã khách hàng </Text>
            <Text style={styles.textSign}>Họ tên </Text>
            <Text style={styles.textSign}>Số điện thoại </Text>
            <Text style={styles.textSign}>Địa chỉ </Text>
          </View>                                           
          <View >                                            
            <TextInput style={{fontSize:20}} value="Triệu Lệ Dĩnh" ></TextInput> 
            <TextInput style={{fontSize:20}} value="012345" ></TextInput>
            <TextInput style={{fontSize:20}} value="0961466005" ></TextInput>
            <TextInput multiline style={{fontSize:20, maxWidth:220}} value="236 Hoàng Quốc Việt, Cổ Nhuế 1, Bắc Từ Liêm" ></TextInput>   
          </View>    
        </View>
        </View>
      </View>
      
    );
};

export default InforCustomScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    backgroundColor: '#ffff',
    alignItems:'center'
  },
  textInput: {
      flex: 1,
      marginTop: Platform.OS === 'ios' ? 0 : -12,
      paddingLeft: 10,
      color: '#05375a',
  },
  button: {
      alignItems: 'center',
      marginTop: 50
  },
  textSign: {
    fontSize:20, marginRight: 15, marginTop:20, fontWeight:'bold'
  }
});

