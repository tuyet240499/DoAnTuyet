import React from 'react';
import { View, Text, Button, StyleSheet, StatusBar, TouchableOpacity } from 'react-native';
import { useTheme } from '@react-navigation/native';
import LinearGradient from 'react-native-linear-gradient';
import {
  Avatar
} from 'react-native-paper';
import FontAwesome from 'react-native-vector-icons/FontAwesome';

const HomeScreen = ({navigation}) => {

  const { colors } = useTheme();

  const theme = useTheme();
  
    return (
      <View style={styles.container}>
        <Avatar.Image style={{marginTop:100}}
                                source={require('./images.jpg')}
                                size={200}/>
        <View style={styles.button}>
                <TouchableOpacity
                    style={styles.signIn}                   
                >
                <LinearGradient
                    colors={['#08d4c4', '#01ab9d']}
                    style={styles.signIn}
                >
                    <Text style={[styles.textSign, {
                        color:'#fff'
                    }]}>CHECK IN</Text>
                </LinearGradient>
                </TouchableOpacity>

                <TouchableOpacity
                    style={[styles.signIn, {
                        marginTop: 20
                    }]}                   
                >
                <LinearGradient
                    colors={['#08d4c4', '#01ab9d']}
                    style={styles.signIn}
                >
                    <Text style={[styles.textSign, {
                        color:'#fff'
                    }]}>TÀI KHOẢN</Text>
                </LinearGradient>
                </TouchableOpacity>
            </View>
      </View>
    );
};

export default HomeScreen;
const styles = StyleSheet.create({
  container: {
    flex: 1, 
    alignItems:'center',
    backgroundColor:'#ffff'
  },
  textInput: {
      flex: 1,
      marginTop: Platform.OS === 'ios' ? 0 : -12,
      paddingLeft: 10,
      color: '#05375a',
  },
  button: {
      alignItems: 'center',
      marginTop: 10, 
      width: 300,
  },
  signIn: {
      width: '100%',
      height: 50,
      justifyContent: 'center',
      alignItems: 'center',
      borderRadius: 10,
  },
  textSign: {
      fontSize: 18,
      fontWeight: 'bold'
  },
});

